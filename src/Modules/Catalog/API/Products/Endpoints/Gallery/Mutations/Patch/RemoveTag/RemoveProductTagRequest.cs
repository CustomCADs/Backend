namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Mutations.Patch.RemoveTag;

public record RemoveProductTagRequest(
	Guid Id,
	Guid TagId
);
