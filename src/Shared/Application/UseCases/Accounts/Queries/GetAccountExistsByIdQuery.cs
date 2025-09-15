namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetAccountExistsByIdQuery(AccountId Id) : IQuery<bool>;
