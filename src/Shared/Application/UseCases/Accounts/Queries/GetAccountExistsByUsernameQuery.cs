namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetAccountExistsByUsernameQuery(string Username) : IQuery<bool>;
