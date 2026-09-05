using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.PurchaseWithDelivery;

public sealed class PurchaseActiveCartWithDeliveryEndpoint(IRequestSender sender)
	: Endpoint<PurchaseActiveCartWithDeliveryRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase-delivery");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Purchase (Delivery)")
			.WithDescription("Purchase all the Items in the Cart (and Ship those marked for Delivery)")
		);
	}

	public override async Task HandleAsync(PurchaseActiveCartWithDeliveryRequest req, CancellationToken ct)
	{
		PaymentDto payment = await sender.SendCommandAsync(
			command: new PurchaseActiveCartWithDeliveryCommand(
				PaymentMethodId: req.PaymentMethodId,
				ShipmentService: req.ShipmentService,
				Address: req.Address,
				Contact: req.Contact,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(payment, x => x.ToResponse()).ConfigureAwait(false);
	}
}
