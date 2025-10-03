namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Patch.Cad;

public sealed record PatchProductCadRequest(
	Guid Id,
	string ContentType,
	decimal Volume
);
