namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Validated;

public sealed record GetValidatedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
