namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Mutations.Post;

public sealed record PostProductRequest(
	string Name,
	string Description,
	decimal Price,
	int CategoryId,
	Guid ImageId,
	Guid CadId
);
