namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Queries.Get.Recent;

public sealed record RecentProductsResponse(
	Guid Id,
	string Name,
	string Status,
	DateTimeOffset UploadedAt,
	CategoryDtoResponse Category
);
