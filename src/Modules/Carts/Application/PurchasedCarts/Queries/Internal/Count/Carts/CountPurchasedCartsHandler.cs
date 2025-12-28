using CustomCADs.Modules.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;

public sealed class CountPurchasedCartsHandler(IPurchasedCartReads reads)
	: IQueryHandler<CountPurchasedCartsQuery, int>
{
	public async Task<int> Handle(CountPurchasedCartsQuery req, CancellationToken ct)
		=> await reads.CountAsync(req.CallerId, ct: ct).ConfigureAwait(false);
}
