namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetAccountViewedProductsByUsernameQuery(
	string Username
) : IQuery<ProductId[]>;
