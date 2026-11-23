namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Mutations.Patch.AddTag;

public record AddProductTagRequest(
	Guid Id,
	Guid TagId
);
