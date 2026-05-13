using CustomCADs.Modules.Accounts.Domain.Accounts;
using CustomCADs.Modules.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Accounts.Persistence.Repositories.Accounts;

internal static class Utilities
{
	extension(IQueryable<Account> query)
	{
		internal IQueryable<Account> WithFilter(AccountId[]? ids, string? role = null)
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

		internal IQueryable<Account> WithSearch(string? username = null, string? email = null, string? firstName = null, string? lastName = null)
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

		internal IQueryable<Account> WithSorting(Sorting<AccountSortingType>? sorting = null)
			=> sorting?.Type switch
			{
				AccountSortingType.Username => query.ToSorted(sorting, x => x.Username),
				AccountSortingType.Email => query.ToSorted(sorting, x => x.Email),
				AccountSortingType.Role => query.ToSorted(sorting, x => x.RoleName),
				_ => query,
			};
	}
}
