namespace CustomCADs.Modules.Printing.API.Customizations;

internal static class Mapper
{
	extension(CustomizationDto customization)
	{
		internal CustomizationResponse ToResponse()
			=> new(
				Id: customization.Id.Value,
				Infill: customization.Infill,
				Volume: customization.Volume,
				Base: customization.Base,
				Scale: customization.Scale,
				Weight: customization.Weight,
				Cost: customization.Cost,
				Color: customization.Color,
				MaterialId: customization.MaterialId.Value
			);
	}

}
