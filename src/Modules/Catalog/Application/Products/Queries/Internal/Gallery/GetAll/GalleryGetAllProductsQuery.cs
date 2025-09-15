using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

[AddRequestCaching(ExpirationType.Absolute, TimeType.Minute, 1)]
public sealed record GalleryGetAllProductsQuery(
	Pagination Pagination,
	AccountId CallerId,
	CategoryId? CategoryId = null,
	TagId[]? TagIds = null,
	string? Name = null,
	Sorting<ProductSortingType>? Sorting = null
) : IQuery<Result<GalleryGetAllProductsDto>>;
