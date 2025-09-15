namespace CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;

public sealed record GetAccountsWithProductInCartQuery(
	ProductId ProductId
) : IQuery<AccountId[]>;
