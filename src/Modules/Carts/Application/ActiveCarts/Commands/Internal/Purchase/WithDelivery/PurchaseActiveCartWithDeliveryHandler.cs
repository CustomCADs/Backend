using CustomCADs.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
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
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public sealed class PurchaseActiveCartWithDeliveryHandler(
	IActiveCartReads reads,
	IRequestSender sender,
	IPaymentService payment,
	IEventRaiser raiser
) : ICommandHandler<PurchaseActiveCartWithDeliveryCommand, PaymentDto>
{
	public async Task<PaymentDto> Handle(PurchaseActiveCartWithDeliveryCommand req, CancellationToken ct)
	{
		if (!await reads.ExistsAsync(req.CallerId, ct).ConfigureAwait(false))
		{
			throw new CustomException("Cart without Items cannot be purchased.");
		}

		ActiveCartItem[] items = await reads.AllAsync(req.CallerId, track: false, ct: ct).ConfigureAwait(false);
		if (!items.Any(x => x.ForDelivery))
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: false);
		}

		CustomizationId[] customizationIds = [..
			items
				.Where(x => x.ForDelivery && x.CustomizationId is not null)
				.Select(x => x.CustomizationId!.Value)
		];
		Dictionary<CustomizationId, decimal> costs = await SnapshotCosts(items, customizationIds, ct).ConfigureAwait(false);

		Dictionary<ProductId, decimal> prices = await SnapshotPrices(items, ct).ConfigureAwait(false);
		decimal totalSum = prices.Sum(x => x.Value) + costs.Sum(x => x.Value);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(req.CallerId),
			ct: ct
		).ConfigureAwait(false);

		PurchasedCartId purchasedCartId = await sender.SendCommandAsync(
			command: new CreatePurchasedCartCommand(
				BuyerId: req.CallerId,
				Items: items.ToDictionary(
					x => x.ToDto(buyer),
					x => x.ForDelivery && x.CustomizationId is not null
							? prices[x.ProductId] + costs[x.CustomizationId.Value]
							: prices[x.ProductId]
				)
			),
			ct: ct
		).ConfigureAwait(false);
		Dictionary<CustomizationId, double> weights = await SnapshotWeights(items, customizationIds, ct).ConfigureAwait(false);

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

		await raiser.RaiseApplicationEventAsync(
			@event: new ActiveCartDeliveryRequestedApplicationEvent(
				PurchasedCartId: purchasedCartId,
				Weight: weights.Sum(x => x.Value),
				Count: items.Where(x => x.ForDelivery).Sum(x => x.Quantity),
				ShipmentService: req.ShipmentService,
				Address: req.Address,
				Contact: req.Contact
			)
		).ConfigureAwait(false);

		return response;
	}

	private async Task<Dictionary<CustomizationId, double>> SnapshotWeights(ActiveCartItem[] items, CustomizationId[] customizationIds, CancellationToken ct)
	{
		Dictionary<CustomizationId, double> weights = await sender.SendQueryAsync(
			query: new GetCustomizationsWeightByIdsQuery(
				Ids: customizationIds
			),
			ct: ct
		).ConfigureAwait(false);

		return weights.ToDictionary(
			x => x.Key,
			x =>
			{
				ActiveCartItem item = items.First(i => i.CustomizationId == x.Key);
				return
					x.Value / 1000 // g -> kg
					* item.Quantity;
			}
		);
	}

	private async Task<Dictionary<CustomizationId, decimal>> SnapshotCosts(ActiveCartItem[] items, CustomizationId[] customizationIds, CancellationToken ct)
	{
		Dictionary<CustomizationId, decimal> costs = await sender.SendQueryAsync(
			query: new GetCustomizationsCostByIdsQuery(
				Ids: customizationIds
			),
			ct: ct
		).ConfigureAwait(false);

		return costs.ToDictionary(
			x => x.Key,
			x =>
			{
				ActiveCartItem item = items.First(i => i.CustomizationId == x.Key);
				return x.Value * item.Quantity;
			}
		);
	}

	private async Task<Dictionary<ProductId, decimal>> SnapshotPrices(ActiveCartItem[] items, CancellationToken ct)
	{
		Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(
			query: new GetProductPricesByIdsQuery(
				Ids: [.. items.Select(x => x.ProductId)]
			),
			ct: ct
		).ConfigureAwait(false);

		return prices.ToDictionary(
			x => x.Key,
			x =>
			{
				ActiveCartItem item = items.First(i => i.ProductId == x.Key);
				return item.ForDelivery ? x.Value * item.Quantity : x.Value;
			}
		);
	}
}
