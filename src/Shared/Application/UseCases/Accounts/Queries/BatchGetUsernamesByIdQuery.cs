namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record BatchGetUsernamesByIdQuery(
	 AccountId[] Ids
) : IQuery<Dictionary<AccountId, string>>;
