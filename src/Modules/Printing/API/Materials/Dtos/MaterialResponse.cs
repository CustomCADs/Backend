namespace CustomCADs.Printing.API.Materials.Dtos;

public sealed record MaterialResponse(
	int Id,
	string Name,
	decimal Density,
	decimal Cost,
	Guid TextureId
);
