namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetAccountViewedProductsByUsernameQuery(
	string Username
) : IQuery<ViewedProductDto[]>;

public sealed record ViewedProductDto(
	ProductId Id,
	DateTimeOffset ViewedAt
);
