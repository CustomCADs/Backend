using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Patch.DecrementQuantity;

public class DecreaeseActiveCartItemQuantityEndpoint(IRequestSender sender)
	: Endpoint<DecreaseActiveCartItemQuantityRequest>
{
	public override void Configure()
	{
		Patch("decrease");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Decrease")
			.WithDescription("Decrease the Cart Item's quantity")
		);
	}

	public override async Task HandleAsync(DecreaseActiveCartItemQuantityRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new DecreaseActiveCartItemQuantityCommand(
				CallerId: User.GetAccountId(),
				ProductId: ProductId.New(req.ProductId),
				Amount: req.Amount
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
