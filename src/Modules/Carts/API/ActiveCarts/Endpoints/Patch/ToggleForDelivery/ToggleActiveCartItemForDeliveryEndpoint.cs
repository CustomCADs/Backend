using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Patch.ToggleForDelivery;

public class ToggleActiveCartItemForDeliveryEndpoint(IRequestSender sender)
	: Endpoint<ToggleActiveCartItemForDeliveryRequest>
{
	public override void Configure()
	{
		Patch("delivery");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Toggle ForDelivery")
			.WithDescription("Turn on/off the Cart Item's planned Delivery")
		);
	}

	public override async Task HandleAsync(ToggleActiveCartItemForDeliveryRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ToggleActiveCartItemForDeliveryCommand(
				CallerId: User.GetAccountId(),
				ProductId: ProductId.New(req.ProductId),
				CustomizationId: CustomizationId.New(req.CustomizationId)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
