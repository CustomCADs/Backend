using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.Count;

public sealed record ProductsCountQuery(
	AccountId CallerId
) : IQuery<ProductsCountDto>;
