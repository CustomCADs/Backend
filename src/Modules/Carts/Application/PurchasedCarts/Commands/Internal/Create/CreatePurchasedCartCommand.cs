using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public sealed record CreatePurchasedCartCommand(
	AccountId BuyerId,
	Dictionary<ActiveCartItemDto, decimal> Items
) : ICommand<PurchasedCartId>;
