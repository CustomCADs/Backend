namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Put.Products;

public sealed record PutProductRequest(
	Guid Id,
	string Name,
	string Description,
	int CategoryId,
	decimal Price,
	CadDto? Cad,
	ImageDto? Image
);
