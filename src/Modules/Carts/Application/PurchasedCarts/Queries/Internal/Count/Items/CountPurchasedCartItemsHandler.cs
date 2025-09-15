using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

public sealed class CountPurchasedCartItemsHandler(IPurchasedCartReads reads)
	: IQueryHandler<CountPurchasedCartItemsQuery, Dictionary<PurchasedCartId, int>>
{
	public async Task<Dictionary<PurchasedCartId, int>> Handle(CountPurchasedCartItemsQuery req, CancellationToken ct)
		=> await reads.CountItemsAsync(req.CallerId, ct: ct).ConfigureAwait(false);
}
