namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Single;

public sealed record DesignerSingleProductResponse(
	Guid Id,
	string Name,
	string Description,
	decimal Price,
	string CreatorName,
	CategoryDtoResponse Category
);
