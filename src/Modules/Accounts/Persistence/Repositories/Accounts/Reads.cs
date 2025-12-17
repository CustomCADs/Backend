using CustomCADs.Modules.Accounts.Domain.Accounts;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Persistence.Extensions;

namespace CustomCADs.Modules.Accounts.Persistence.Repositories.Accounts;

public sealed class Reads(AccountsContext context) : IAccountReads
{
	public async Task<Result<Account>> AllAsync(AccountQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Account> queryable = context.Accounts
			.WithTracking(track)
			.WithFilter(query.Ids, query.Role)
			.WithSearch(query.Username, query.Email, query.FirstName, query.LastName);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Account[] accounts = await queryable
			.WithSorting(query.Sorting)
			.WithPagination(query.Pagination)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, accounts);
	}

	public async Task<ICollection<AccountId>> AllIdsByRoleAsync(string role, CancellationToken ct = default)
		=> await context.Accounts
			.Where(x => x.RoleName == role)
			.Select(x => x.Id)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Account?> SingleByIdAsync(AccountId id, bool track = true, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Account?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Username == username, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(AccountId id, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(false)
			.AnyAsync(x => x.Username == username, ct)
			.ConfigureAwait(false);

	public async Task<ProductId[]> ViewedProductsByIdAsync(AccountId id, CancellationToken ct = default)
		=> await context.ViewedProducts
			.GetViewedProductsByAccountIdAsync(id, ct)
			.ConfigureAwait(false);

	public async Task<ProductId[]> ViewedProductsByUsernameAsync(string username, CancellationToken ct = default)
		=> await context.ViewedProducts
			.GetViewedProductsByAccountUsernrameAsync(username, ct)
			.ConfigureAwait(false);

	public async Task<int> CountAsync(CancellationToken ct = default)
		=> await context.Accounts
			.WithTracking(false)
			.CountAsync(ct)
			.ConfigureAwait(false);
}
