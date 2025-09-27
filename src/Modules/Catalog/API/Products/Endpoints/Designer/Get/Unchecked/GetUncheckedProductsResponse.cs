namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Unchecked;

public sealed record GetUncheckedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
