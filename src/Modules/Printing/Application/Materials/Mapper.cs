namespace CustomCADs.Modules.Printing.Application.Materials;

internal static class Mapper
{
	extension(Material material)
	{
		public MaterialDto ToDto()
			=> new(
				Id: material.Id,
				Name: material.Name,
				Density: material.Density,
				Cost: material.Cost,
				TextureId: material.TextureId
			);
	}

}
