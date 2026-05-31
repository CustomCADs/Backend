using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using Stripe;

namespace CustomCADs.Shared.Infrastructure.Payment;

using static Messages;

public sealed class StripeService(PaymentIntentService service) : IPaymentService
{
	private static PaymentIntentCreateOptions CreateOptions(
		decimal total,
		(string MethodId, string BuyerId) payment,
		(string Type, string Id) reward,
		string description
	)
		=> new()
		{
			Amount = Convert.ToInt64(total * 100),
			Currency = "EUR",
			PaymentMethod = payment.MethodId,
			Description = description,
			Confirm = true,
			AutomaticPaymentMethods = new()
			{
				Enabled = true,
				AllowRedirects = "never"
			},
			Metadata = new()
			{
				["buyerId"] = payment.BuyerId,
				["rewardType"] = reward.Type,
				["rewardId"] = reward.Id,
			},
		};

	public async Task<PaymentDto> InitializeCartPayment(
		string paymentMethodId,
		AccountId buyerId,
		PurchasedCartId cartId,
		decimal total,
		(string Buyer, int ItemsCount) description,
		CancellationToken ct = default
	) => await HandleResponseAsync(
		intent: await service.CreateAsync(
			options: CreateOptions(
				total: total,
				payment: (MethodId: paymentMethodId, BuyerId: buyerId.ToString()),
				reward: (Type: "cart", Id: cartId.ToString()),
				description: $"{description.Buyer} bought {description.ItemsCount} items for a total of {total}€."
			),
			cancellationToken: ct
		).ConfigureAwait(false),
		ct
	).ConfigureAwait(false);

	public async Task<PaymentDto> InitializeCustomPayment(
		string paymentMethodId,
		AccountId buyerId,
		CustomId customId,
		decimal total,
		(string Buyer, string Name, string Seller) description,
		CancellationToken ct = default
	) => await HandleResponseAsync(
		intent: await service.CreateAsync(
			options: CreateOptions(
				total: total,
				payment: (MethodId: paymentMethodId, BuyerId: buyerId.ToString()),
				reward: (Type: "custom", Id: customId.ToString()),
				description: $"{description.Buyer} bought {description.Name} from {description.Seller} for a total of {total}€."
			),
			cancellationToken: ct
		).ConfigureAwait(false),
		ct
	).ConfigureAwait(false);

	private async Task<PaymentDto> HandleResponseAsync(PaymentIntent intent, CancellationToken ct)
	{
		PaymentDto response = new(
			ClientSecret: intent.ClientSecret,
			Message: GetMessageFromStatus(intent.Status)
		);

		switch (response.Message)
		{
			case FailedPaymentCapture:
				intent = await service.CaptureAsync(intent.Id, cancellationToken: ct).ConfigureAwait(false);
				response = response with { Message = GetMessageFromStatus(intent.Status) };

				if (response.Message == SuccessfulPayment)
				{
					return response;
				}
				throw PaymentFailedException.WithClientSecret(intent.ClientSecret, response.Message);

			case ProcessingPayment:
			case SuccessfulPayment:
				return response;

			default:
				throw PaymentFailedException.General(response.Message);
		}

		static string GetMessageFromStatus(string status)
			=> status switch
			{
				"succeeded" => SuccessfulPayment,
				"processing" => ProcessingPayment,
				"canceled" => CanceledPayment,
				"requires_payment_method" => FailedPaymentMethod,
				"requires_action" => FailedPayment,
				"requires_capture" => FailedPaymentCapture,
				_ => string.Format(UnhandledPayment, status)
			};
	}
}
