using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;

public sealed record DecreaseActiveCartItemQuantityCommand(
	AccountId CallerId,
	ProductId ProductId,
	int Amount
) : ICommand<int>;
