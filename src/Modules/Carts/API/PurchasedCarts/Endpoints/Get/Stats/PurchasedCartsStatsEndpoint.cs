using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;
using CustomCADs.Shared.Domain.TypedIds.Carts;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Get.Stats;

public sealed class PurchasedCartsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<PurchasedCartsStatsResponse, PurchasedCartsStatsMappper>
{
	public override void Configure()
	{
		Get("stats");
		Group<PurchasedCartsGroup>();
		Description(x => x
			.WithSummary("Stats")
			.WithDescription("See your Carts' Stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		int totalCartCount = await sender.SendQueryAsync(
			query: new CountPurchasedCartsQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		Dictionary<PurchasedCartId, int> counts = await sender.SendQueryAsync(
			query: new CountPurchasedCartItemsQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync((totalCartCount, counts), Map.FromEntity).ConfigureAwait(false);
	}
}
