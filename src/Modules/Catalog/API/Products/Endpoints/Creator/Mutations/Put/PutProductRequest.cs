namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Mutations.Put;

public sealed record PutProductRequest(
	Guid Id,
	string Name,
	string Description,
	int CategoryId,
	decimal Price
);
