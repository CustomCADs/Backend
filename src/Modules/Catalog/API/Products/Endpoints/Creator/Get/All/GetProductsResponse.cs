namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.All;

public sealed record GetProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	CategoryDtoResponse Category,
	Guid ImageId
);
