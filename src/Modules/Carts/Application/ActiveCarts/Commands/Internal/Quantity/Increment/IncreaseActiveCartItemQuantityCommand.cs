using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;

public sealed record IncreaseActiveCartItemQuantityCommand(
	AccountId CallerId,
	ProductId ProductId,
	int Amount
) : ICommand<int>;
