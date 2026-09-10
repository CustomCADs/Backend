namespace CustomCADs.Modules.Printing.Application.Customizations;

internal static class Mapper
{
	extension(Customization customization)
	{
		internal CustomizationDto ToDto(decimal weight, decimal cost)
			=> new(
				Id: customization.Id,
				Infill: customization.Infill,
				Volume: customization.Volume.Value,
				Base: customization.Volume.Base,
				Scale: customization.Volume.Scale,
				Weight: weight,
				Cost: cost,
				Color: customization.Color,
				MaterialId: customization.MaterialId
			);
	}

}
