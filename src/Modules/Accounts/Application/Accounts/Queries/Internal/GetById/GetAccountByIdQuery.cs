namespace CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetById;

public sealed record GetAccountByIdQuery(
	AccountId Id
) : IQuery<GetAccountByIdDto>;
