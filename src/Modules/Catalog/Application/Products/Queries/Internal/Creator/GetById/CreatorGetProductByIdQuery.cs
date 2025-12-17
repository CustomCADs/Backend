using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;

public sealed record CreatorGetProductByIdQuery(
	ProductId Id,
	AccountId CallerId
) : IQuery<CreatorGetProductByIdDto>;
