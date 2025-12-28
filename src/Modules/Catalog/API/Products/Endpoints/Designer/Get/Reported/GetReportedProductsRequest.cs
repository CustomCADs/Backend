using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Get.Reported;

public sealed record GetReportedProductsRequest(
	int? CategoryId = null,
	string? Name = null,
	ProductDesignerSortingType SortingType = ProductDesignerSortingType.UploadedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
