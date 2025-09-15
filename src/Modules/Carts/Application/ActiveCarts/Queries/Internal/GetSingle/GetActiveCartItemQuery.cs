using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

public sealed record GetActiveCartItemQuery(
	AccountId CallerId,
	ProductId ProductId
) : IQuery<ActiveCartItemDto>;
