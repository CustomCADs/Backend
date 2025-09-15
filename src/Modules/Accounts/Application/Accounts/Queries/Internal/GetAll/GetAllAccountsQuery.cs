using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;

public sealed record GetAllAccountsQuery(
	Pagination Pagination,
	string? Role = null,
	string? Username = null,
	string? Email = null,
	string? FirstName = null,
	string? LastName = null,
	Sorting<AccountSortingType>? Sorting = null
) : IQuery<Result<GetAllAccountsDto>>;
