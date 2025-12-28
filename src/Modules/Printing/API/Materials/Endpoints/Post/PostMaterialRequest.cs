namespace CustomCADs.Modules.Printing.API.Materials.Endpoints.Post;

public sealed record PostMaterialRequest(
	string Name,
	decimal Density,
	decimal Cost,
	Guid TextureId
);
