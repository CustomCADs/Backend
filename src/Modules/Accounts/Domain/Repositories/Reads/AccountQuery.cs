using CustomCADs.Modules.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Accounts.Domain.Repositories.Reads;

public record AccountQuery(
	Pagination Pagination,
	AccountId[]? Ids = null,
	string? Role = null,
	string? Username = null,
	string? Email = null,
	string? FirstName = null,
	string? LastName = null,
	Sorting<AccountSortingType>? Sorting = null
);
