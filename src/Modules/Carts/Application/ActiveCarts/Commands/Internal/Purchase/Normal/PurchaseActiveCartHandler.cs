using CustomCADs.Carts.Application.ActiveCarts.Events.Application.PaymentStarted;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

public sealed class PurchaseActiveCartHandler(
	IActiveCartReads reads,
	IRequestSender sender,
	IPaymentService payment,
	IEventRaiser raiser
) : ICommandHandler<PurchaseActiveCartCommand, PaymentDto>
{
	public async Task<PaymentDto> Handle(PurchaseActiveCartCommand req, CancellationToken ct)
	{
		if (!await reads.ExistsAsync(req.CallerId, ct).ConfigureAwait(false))
		{
			throw new CustomException("Cart without Items cannot be purchased.");
		}

		ActiveCartItem[] items = await reads.AllAsync(req.CallerId, track: false, ct: ct).ConfigureAwait(false);
		if (items.Any(x => x.ForDelivery))
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);
		}

		Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(
			query: new GetProductPricesByIdsQuery(
				Ids: [.. items.Select(x => x.ProductId)]
			),
			ct: ct
		).ConfigureAwait(false);
		decimal totalSum = prices.Sum(x => x.Value);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(req.CallerId),
			ct: ct
		).ConfigureAwait(false);

		PurchasedCartId purchasedCartId = await sender.SendCommandAsync(
			command: new CreatePurchasedCartCommand(
				BuyerId: req.CallerId,
				Items: items.ToDictionary(
					x => x.ToDto(buyer),
					x => prices[x.ProductId]
				)
			),
			ct: ct
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new NotificationRequestedEvent(
				Type: NotificationType.CartPurchased,
				Description: ApplicationConstants.Notifications.Messages.CartPurchased,
				Link: ApplicationConstants.Notifications.Links.CartPurchased,
				AuthorId: req.CallerId,
				ReceiverIds: [req.CallerId]
			)
		).ConfigureAwait(false);

		PaymentDto response = await payment.InitializeCartPayment(
			paymentMethodId: req.PaymentMethodId,
			buyerId: req.CallerId,
			cartId: purchasedCartId,
			total: totalSum,
			description: (buyer, items.Length),
			ct: ct
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new CartPaymentStartedApplicationEvent(purchasedCartId.Value)
		).ConfigureAwait(false);

		return response;
	}
}
