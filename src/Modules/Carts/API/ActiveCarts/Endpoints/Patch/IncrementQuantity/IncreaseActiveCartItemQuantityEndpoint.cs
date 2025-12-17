using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Patch.IncrementQuantity;

public class IncreaseActiveCartItemQuantityEndpoint(IRequestSender sender)
	: Endpoint<IncreaseActiveCartItemQuantityRequest>
{
	public override void Configure()
	{
		Patch("increase");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Increase")
			.WithDescription("Increase the Cart Item's quantity")
		);
	}

	public override async Task HandleAsync(IncreaseActiveCartItemQuantityRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new IncreaseActiveCartItemQuantityCommand(
				CallerId: User.AccountId,
				ProductId: ProductId.New(req.ProductId),
				Amount: req.Amount
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
