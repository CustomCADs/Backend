using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetAll;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Get.All;

public sealed class GetActiveCartItemsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ICollection<ActiveCartItemResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all your Cart Items")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ActiveCartItemDto[] cart = await sender.SendQueryAsync(
			query: new GetActiveCartItemsQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		ICollection<ActiveCartItemResponse> response = [.. cart.Select(x => x.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
