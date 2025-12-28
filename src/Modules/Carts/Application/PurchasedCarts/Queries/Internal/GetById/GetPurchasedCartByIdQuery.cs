using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

public sealed record GetPurchasedCartByIdQuery(
	PurchasedCartId Id,
	AccountId CallerId
) : IQuery<GetPurchasedCartByIdDto>;
