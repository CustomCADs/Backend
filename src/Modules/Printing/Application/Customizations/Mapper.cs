namespace CustomCADs.Modules.Printing.Application.Customizations;

internal static class Mapper
{
	extension(Customization customization)
	{
		internal CustomizationDto ToDto(decimal weight, decimal cost)
			=> new(
				Id: customization.Id,
				Scale: customization.Scale,
				Infill: customization.Infill,
				Volume: customization.Volume,
				Weight: weight,
				Cost: cost,
				Color: customization.Color,
				MaterialId: customization.MaterialId
			);
	}

}
