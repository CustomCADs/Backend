using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Catalog.Domain.Repositories.Reads;

public record ProductQuery(
	Pagination Pagination,
	ProductId[]? Ids = null,
	TagId[]? TagIds = null,
	AccountId? DesignerId = null,
	AccountId? CreatorId = null,
	CategoryId? CategoryId = null,
	ProductStatus? Status = null,
	string? Name = null,
	Sorting<ProductSortingType>? Sorting = null
);
