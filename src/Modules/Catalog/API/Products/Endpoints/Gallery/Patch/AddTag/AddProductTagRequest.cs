namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Patch.AddTag;

public record AddProductTagRequest(
	Guid Id,
	Guid TagId
);
