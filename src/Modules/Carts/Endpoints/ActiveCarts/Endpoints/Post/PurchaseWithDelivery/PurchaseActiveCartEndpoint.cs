using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.PurchaseWithDelivery;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
	: Endpoint<PurchaseActiveCartRequest, PaymentResponse>
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

	public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
	{
		PaymentDto dto = await sender.SendCommandAsync(
			command: new PurchaseActiveCartWithDeliveryCommand(
				PaymentMethodId: req.PaymentMethodId,
				ShipmentService: req.ShipmentService,
				Address: req.Address,
				Contact: req.Contact,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		PaymentResponse response = dto.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
