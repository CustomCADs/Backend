using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

[AddRequestCaching(ExpirationType.Sliding, TimeType.Hour, 8)]
public record GetAccountIdsByRoleQuery(
	string Role
) : IQuery<ICollection<AccountId>>;
