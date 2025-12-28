namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Post;

public sealed record PostProductRequest(
	string Name,
	string Description,
	decimal Price,
	int CategoryId,
	Guid ImageId,
	Guid CadId
);
