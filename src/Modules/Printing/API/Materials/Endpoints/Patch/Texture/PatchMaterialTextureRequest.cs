namespace CustomCADs.Printing.API.Materials.Endpoints.Patch.Texture;

public sealed record PatchMaterialTextureRequest(
	int Id,
	string ContentType
);
