namespace CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;

public record GetAccountsWithProductInCartQuery(
	ProductId ProductId
) : IQuery<AccountId[]>;
