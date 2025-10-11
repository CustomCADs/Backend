namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.All;

public sealed record GetAllGalleryProductsResponse(
	Guid Id,
	string Name,
	string[] Tags,
	string Category,
	int Views,
	Guid ImageId
);
