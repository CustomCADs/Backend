using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

public sealed record GetActiveCartItemsQuery(
	AccountId CallerId
) : IQuery<ActiveCartItemDto[]>;
