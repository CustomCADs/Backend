using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Accounts.Persistence.ShadowEntities;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Accounts.Persistence.Repositories.Accounts;

public static class Utilities
{
	public static IQueryable<Account> WithFilter(this IQueryable<Account> query, AccountId[]? ids, string? role = null)
	{
		if (!string.IsNullOrEmpty(role))
		{
			query = query.Where(x => x.RoleName == role);
		}
		if (ids is not null)
		{
			query = query.Where(x => ids.Contains(x.Id));
		}

		return query;
	}

	public static IQueryable<Account> WithSearch(this IQueryable<Account> query, string? username = null, string? email = null, string? firstName = null, string? lastName = null)
	{
		if (!string.IsNullOrWhiteSpace(username))
		{
			query = query.Where(x => x.Username.ToLower().Contains(username.ToLower()));
		}
		if (!string.IsNullOrWhiteSpace(email))
		{
			query = query.Where(x => x.Email.Contains(email));
		}
		if (!string.IsNullOrWhiteSpace(firstName))
		{
			query = query.Where(x => x.FirstName != null && x.FirstName.ToLower().Contains(firstName.ToLower()));
		}
		if (!string.IsNullOrWhiteSpace(lastName))
		{
			query = query.Where(x => x.LastName != null && x.LastName.ToLower().Contains(lastName.ToLower()));
		}

		return query;
	}

	public static IQueryable<Account> WithSorting(this IQueryable<Account> query, Sorting<AccountSortingType>? sorting = null)
		=> sorting?.Type switch
		{
			AccountSortingType.Username => query.ToSorted(sorting, x => x.Username),
			AccountSortingType.Email => query.ToSorted(sorting, x => x.Email),
			AccountSortingType.Role => query.ToSorted(sorting, x => x.RoleName),
			_ => query,
		};

	public static async Task<ProductId[]> GetViewedProductsByAccountIdAsync(this DbSet<ViewedProduct> set, AccountId id, CancellationToken ct = default)
		=> await set
			.Where(x => x.AccountId == id)
			.Select(x => x.ProductId)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public static async Task<ProductId[]> GetViewedProductsByAccountUsernrameAsync(this DbSet<ViewedProduct> set, string username, CancellationToken ct = default)
		=> await set
			.Include(x => x.Account)
			.Where(x => x.Account.Username == username)
			.Select(x => x.ProductId)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);
}
