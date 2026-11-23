using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToOffice;
using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.PurchaseWithDelivery.ToOffice;

public sealed class PurchaseActiveCartWithDeliveryToOfficeEndpoint(IRequestSender sender)
	: Endpoint<PurchaseActiveCartWithDeliveryToOfficeRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase-delivery/office");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Purchase (Office Delivery)")
			.WithDescription("Purchase all the Items in the Cart (and Ship those marked for Delivery to your Office)")
		);
	}

	public override async Task HandleAsync(PurchaseActiveCartWithDeliveryToOfficeRequest req, CancellationToken ct)
	{
		PaymentDto payment = await sender.SendCommandAsync(
			command: new PurchaseActiveCartWithDeliveryToOfficeCommand(
				PaymentMethodId: req.PaymentMethodId,
				ShipmentService: req.ShipmentService,
				OfficeId: req.OfficeId,
				Contact: req.Contact,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(payment, x => x.ToResponse()).ConfigureAwait(false);
	}
}
