using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

public sealed record CountPurchasedCartItemsQuery(
	AccountId CallerId
) : IQuery<Dictionary<PurchasedCartId, int>>;
