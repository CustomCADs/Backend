using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;

public sealed record CountPurchasedCartsQuery(
	AccountId CallerId
) : IQuery<int>;
