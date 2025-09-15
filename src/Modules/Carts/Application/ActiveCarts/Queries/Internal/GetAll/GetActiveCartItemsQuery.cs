using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

public sealed record GetActiveCartItemsQuery(
	AccountId CallerId
) : IQuery<ActiveCartItemDto[]>;
