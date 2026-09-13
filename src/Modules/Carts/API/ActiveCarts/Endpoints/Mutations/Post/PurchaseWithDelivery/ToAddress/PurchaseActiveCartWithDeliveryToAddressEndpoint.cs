using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToAddress;
using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.PurchaseWithDelivery.ToAddress;

public sealed class PurchaseActiveCartWithDeliveryToAddressEndpoint(IRequestSender sender)
	: Endpoint<PurchaseActiveCartWithDeliveryToAddressRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase-delivery/address");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Purchase (Address Delivery)")
			.WithDescription("Purchase all the Items in the Cart (and Ship those marked for Delivery to your address)")
		);
	}

	public override async Task HandleAsync(PurchaseActiveCartWithDeliveryToAddressRequest req, CancellationToken ct)
	{
		PaymentDto payment = await sender.SendCommandAsync(
			command: new PurchaseActiveCartWithDeliveryToAddressCommand(
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
