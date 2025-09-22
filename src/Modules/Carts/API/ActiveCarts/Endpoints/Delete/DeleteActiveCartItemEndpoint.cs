using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Remove;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Delete;

public sealed class DeleteActiveCartItemEndpoint(IRequestSender sender)
	: Endpoint<DeleteActiveCartItemRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Remove Item")
			.WithDescription("Remove an Item from your Cart")
		);
	}

	public override async Task HandleAsync(DeleteActiveCartItemRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new RemoveActiveCartItemCommand(
				ProductId: ProductId.New(req.ProductId),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
