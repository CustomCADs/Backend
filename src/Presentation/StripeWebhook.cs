using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Carts;
using CustomCADs.Shared.Application.Events.Customs;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Infrastructure.Payment;
using Microsoft.Extensions.Options;
using Stripe;

namespace CustomCADs.Presentation;

using static Shared.Endpoints.EndpointsConstants;

public static class StripeWebhook
{
	public static void MapStripeWebhook(this IEndpointRouteBuilder app)
	{
		app.MapPost($"api/{Paths.Stripe}/webhook", async (HttpContext context, IEventRaiser raiser, IOptions<PaymentSettings> options) =>
		{
			IResult? result = ExtractEvent(
				json: await new StreamReader(context.Request.Body).ReadToEndAsync().ConfigureAwait(false),
				signature: context.Request.Headers["Stripe-Signature"],
				secret: options.Value.WebhookSecret,
				out Event stripeEvent
			);
			if (result is not null) { return result; }

			result = stripeEvent.Destructure(
				out PaymentIntent intent,
				out AccountId buyerId
			);
			if (result is not null) { return result; }

			if (stripeEvent.Type is EventTypes.PaymentIntentPaymentFailed)
			{
				await raiser.RaiseApplicationEventAsync(
					new NotificationRequestedEvent(
						Type: NotificationType.PaymentFailed,
						Description: Shared.Application.ApplicationConstants.Notifications.Messages.PaymentFailed,
						Link: Shared.Application.ApplicationConstants.Notifications.Links.PaymentFailed,
						AuthorId: buyerId,
						ReceiverIds: [buyerId]
					)
				).ConfigureAwait(false);
			}
			else if (stripeEvent.Type is EventTypes.PaymentIntentSucceeded)
			{
				IResult resultMissingRewardId = Results.BadRequest("Missing RewardId");
				string rewardType = intent.Metadata["rewardType"];
				switch (rewardType)
				{
					case "cart":
						{
							PurchasedCartId? rewardId = PurchasedCartId.New(intent.Metadata["rewardId"]);
							if (rewardId is null)
							{
								return resultMissingRewardId;
							}

							await raiser.RaiseApplicationEventAsync(
								@event: new CartPaymentCompletedApplicationEvent(
									Id: rewardId.Value,
									BuyerId: buyerId
								)
							).ConfigureAwait(false);
							break;
						}

					case "custom":
						{
							CustomId? rewardId = CustomId.New(intent.Metadata["rewardId"]);
							if (rewardId is null)
							{
								return resultMissingRewardId;
							}

							await raiser.RaiseApplicationEventAsync(
								@event: new CustomPaymentCompletedApplicationEvent(
									Id: rewardId.Value,
									BuyerId: buyerId
								)
							).ConfigureAwait(false);
							break;
						}
				}
				await raiser.RaiseApplicationEventAsync(
					new NotificationRequestedEvent(
						Type: NotificationType.PaymentCompleted,
						Description: Shared.Application.ApplicationConstants.Notifications.Messages.PaymentCompleted,
						Link: Shared.Application.ApplicationConstants.Notifications.Links.PaymentCompleted,
						AuthorId: buyerId,
						ReceiverIds: [buyerId]
					)
				).ConfigureAwait(false);
			}
			else { /* Log unexpected type: stripeEvent.Type */ }

			return Results.Ok();
		})
		.WithTags(Tags[Paths.Stripe])
		.WithSummary("Stripe Webhook")
		.WithDescription("Not meant for the client to use")
		.WithMetadata(new SkipIdempotencyAttribute());
	}

	private static IResult? Destructure(this Event stripeEvent, out PaymentIntent paymentIntent, out AccountId buyerId)
	{
		paymentIntent = default!;
		buyerId = default;

		if (stripeEvent.Data.Object is not PaymentIntent intent)
		{
			return Results.BadRequest("Invalid PaymentIntent object.");
		}
		paymentIntent = intent;

		AccountId id = AccountId.New(intent.Metadata["buyerId"]);
		if (id.IsEmpty())
		{
			return Results.BadRequest("Invalid BuyerId metadata");
		}
		buyerId = id;

		return null;
	}

	private static IResult? ExtractEvent(in string json, in string? signature, in string secret, out Event stripeEvent)
	{
		try
		{
			stripeEvent = EventUtility.ConstructEvent(
				json: json,
				stripeSignatureHeader: signature,
				secret: secret
			);
			return null;
		}
		catch (StripeException e)
		{
			stripeEvent = new();
			return Results.BadRequest($"Signature verification failed: {e.Message}");
		}
	}
}
