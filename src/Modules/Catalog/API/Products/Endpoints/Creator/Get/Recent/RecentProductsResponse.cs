namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Recent;

public sealed record RecentProductsResponse(
	Guid Id,
	string Name,
	string Status,
	DateTimeOffset UploadedAt,
	CategoryDtoResponse Category
);
