using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Post.Purchase;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
	: Endpoint<PurchaseActiveCartRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Purchase")
			.WithDescription("Purchase all the Items in the Cart")
		);
	}

	public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
	{
		PaymentDto payment = await sender.SendCommandAsync(
			command: new PurchaseActiveCartCommand(
				PaymentMethodId: req.PaymentMethodId,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(payment, x => x.ToResponse()).ConfigureAwait(false);
	}
}
