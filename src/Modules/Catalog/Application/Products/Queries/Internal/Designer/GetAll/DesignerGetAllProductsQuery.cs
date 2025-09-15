using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

public sealed record DesignerGetAllProductsQuery(
	Pagination Pagination,
	AccountId CallerId,
	ProductStatus Status,
	CategoryId? CategoryId = null,
	TagId[]? TagIds = null,
	string? Name = null,
	Sorting<ProductSortingType>? Sorting = null
) : IQuery<Result<DesignerGetAllProductsDto>>;
