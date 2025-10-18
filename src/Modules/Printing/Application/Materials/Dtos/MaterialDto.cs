using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Printing.Application.Materials.Dtos;

public record MaterialDto(
	MaterialId Id,
	string Name,
	decimal Density,
	decimal Cost,
	ImageId TextureId
);
