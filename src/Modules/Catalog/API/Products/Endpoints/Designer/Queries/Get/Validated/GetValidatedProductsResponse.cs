namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Queries.Get.Validated;

public sealed record GetValidatedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
