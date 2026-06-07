namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Mutations.Post;

public sealed record PostProductResponse(
	Guid Id,
	string Name,
	string Description,
	string CreatorName,
	DateTimeOffset UploadedAt,
	decimal Price,
	string Status,
	CategoryDtoResponse Category,
	Guid CadId,
	Guid ImageId
);
