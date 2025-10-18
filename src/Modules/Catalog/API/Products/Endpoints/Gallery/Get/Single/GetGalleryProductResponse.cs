namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.Single;

public sealed record GetGalleryProductResponse(
	Guid Id,
	string Name,
	string Description,
	decimal Price,
	string[] Tags,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CountsDto Counts,
	CategoryDtoResponse Category,
	Guid ImageId,
	Guid CadId
);
