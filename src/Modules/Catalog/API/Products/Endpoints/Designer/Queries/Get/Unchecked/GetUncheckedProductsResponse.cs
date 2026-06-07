namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Queries.Get.Unchecked;

public sealed record GetUncheckedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
