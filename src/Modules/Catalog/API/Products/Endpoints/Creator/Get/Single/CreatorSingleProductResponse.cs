namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Single;

public sealed record CreatorSingleProductResponse(
	Guid Id,
	string Name,
	string Description,
	decimal Price,
	DateTimeOffset UploadedAt,
	CountsDto Counts,
	CategoryDtoResponse Category
);
