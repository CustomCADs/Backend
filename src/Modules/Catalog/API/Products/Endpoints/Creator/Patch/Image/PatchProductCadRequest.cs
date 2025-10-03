namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Patch.Image;

public sealed record PatchProductImageRequest(
	Guid Id,
	string ContentType
);
