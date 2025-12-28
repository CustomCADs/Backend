namespace CustomCADs.Modules.Printing.API.Customizations;

internal static class Mapper
{
	extension(CustomizationDto customization)
	{
		internal CustomizationResponse ToResponse()
			=> new(
				Id: customization.Id.Value,
				Scale: customization.Scale,
				Infill: customization.Infill,
				Weight: customization.Weight,
				Cost: customization.Cost,
				Color: customization.Color,
				MaterialId: customization.MaterialId.Value
			);
	}

}
