namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetAccountViewedProductQuery(
	AccountId Id,
	ProductId ProductId
) : IQuery<bool>;
