using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetById;

public sealed record DesignerGetProductByIdQuery(
	ProductId Id,
	AccountId CallerId
) : IQuery<DesignerGetProductByIdDto>;
