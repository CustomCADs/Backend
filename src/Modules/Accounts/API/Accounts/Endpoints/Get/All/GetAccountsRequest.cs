using CustomCADs.Modules.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Modules.Accounts.API.Accounts.Endpoints.Get.All;

public sealed record GetAccountsRequest(
	string? Name = default,
	AccountSortingType SortingType = AccountSortingType.CreationDate,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 50
);
