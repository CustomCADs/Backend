using CustomCADs.Modules.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.Count;

public sealed class CountActiveCartItemsHandler(IActiveCartReads reads)
	: IQueryHandler<CountActiveCartItemsQuery, int>
{
	public async Task<int> Handle(CountActiveCartItemsQuery req, CancellationToken ct)
		=> await reads.CountAsync(req.CallerId, ct: ct).ConfigureAwait(false);
}
