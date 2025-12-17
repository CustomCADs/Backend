namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Patch.RemoveTag;

public record RemoveProductTagRequest(
	Guid Id,
	Guid TagId
);
