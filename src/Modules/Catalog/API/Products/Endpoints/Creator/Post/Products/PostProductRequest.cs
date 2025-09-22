namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Post.Products;

public sealed record PostProductRequest(
	string Name,
	string Description,
	int CategoryId,
	decimal Price,
	string ImageKey,
	string ImageContentType,
	string CadKey,
	string CadContentType,
	decimal CadVolume
);
