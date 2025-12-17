using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Get.All;

public sealed record GetAllGalleryProductsRequest(
	int? CategoryId = null,
	Guid[]? TagIds = null,
	string? Name = null,
	ProductGallerySortingType SortingType = ProductGallerySortingType.UploadedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
