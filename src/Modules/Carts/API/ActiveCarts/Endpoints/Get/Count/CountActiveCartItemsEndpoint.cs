using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.Count;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Get.Count;

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
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(count).ConfigureAwait(false);
	}
}
