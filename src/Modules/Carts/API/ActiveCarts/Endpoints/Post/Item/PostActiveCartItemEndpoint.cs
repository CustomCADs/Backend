using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;
using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Post.Item;

public sealed class PostActiveCartItemEndpoint(IRequestSender sender)
	: Endpoint<PostActiveCartItemRequest, ActiveCartItemResponse>
{
	public override void Configure()
	{
		Post("");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Add Item")
			.WithDescription("Add an Item to your Cart")
		);
	}

	public override async Task HandleAsync(PostActiveCartItemRequest req, CancellationToken ct)
	{
		AccountId callerId = User.GetAccountId();

		await sender.SendCommandAsync(
			command: new AddActiveCartItemCommand(
				ProductId: ProductId.New(req.ProductId),
				ForDelivery: req.ForDelivery,
				CustomizationId: CustomizationId.New(req.CustomizationId),
				CallerId: callerId
			),
			ct: ct
		).ConfigureAwait(false);

		ActiveCartItemDto item = await sender.SendQueryAsync(
			query: new GetActiveCartItemQuery(
				CallerId: callerId,
				ProductId: ProductId.New(req.ProductId)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(item, x => x.ToResponse()).ConfigureAwait(false);
	}
}
