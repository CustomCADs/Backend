using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.Count;

public sealed record CountActiveCartItemsQuery(
	AccountId CallerId
) : IQuery<int>;
