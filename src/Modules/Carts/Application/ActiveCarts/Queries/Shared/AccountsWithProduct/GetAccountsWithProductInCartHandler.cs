using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Shared.AccountsWithProduct;

public sealed class GetAccountsWithProductInCartHandler(IActiveCartReads reads)
	: IQueryHandler<GetAccountsWithProductInCartQuery, AccountId[]>
{
	public async Task<AccountId[]> Handle(GetAccountsWithProductInCartQuery req, CancellationToken ct = default)
		=> await reads.AccountsWithAsync(req.ProductId, ct).ConfigureAwait(false);
}
