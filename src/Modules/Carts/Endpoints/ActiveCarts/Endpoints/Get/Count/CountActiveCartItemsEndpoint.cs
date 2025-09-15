using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.Count;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.Count;

public sealed class CountActiveCartItemsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<int>
{
	public override void Configure()
	{
		Get("count");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Count")
			.WithDescription("Count your Cart Items")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		int count = await sender.SendQueryAsync(
			query: new CountActiveCartItemsQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(count).ConfigureAwait(false);
	}
}
