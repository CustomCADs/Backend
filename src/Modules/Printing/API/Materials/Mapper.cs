using CustomCADs.Modules.Printing.API.Materials.Dtos;

namespace CustomCADs.Modules.Printing.API.Materials;

internal static class Mapper
{
	extension(MaterialDto material)
	{
		internal MaterialResponse ToResponse()
			=> new(
				Id: material.Id.Value,
				Name: material.Name,
				Density: material.Density,
				Cost: material.Cost,
				TextureId: material.TextureId.Value
			);
	}

}
