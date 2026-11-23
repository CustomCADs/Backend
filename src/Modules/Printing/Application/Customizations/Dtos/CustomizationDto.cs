namespace CustomCADs.Modules.Printing.Application.Customizations.Dtos;

public record CustomizationDto(
	CustomizationId Id,
	decimal Infill,
	decimal Volume,
	decimal Base,
	decimal Scale,
	decimal Weight,
	decimal Cost,
	string Color,
	MaterialId MaterialId
);
