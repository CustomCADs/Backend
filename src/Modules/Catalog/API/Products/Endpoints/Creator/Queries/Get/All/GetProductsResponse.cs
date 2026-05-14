namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Queries.Get.All;

public sealed record GetProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	CategoryDtoResponse Category,
	Guid ImageId
);
