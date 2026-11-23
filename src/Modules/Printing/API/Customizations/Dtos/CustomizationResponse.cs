namespace CustomCADs.Modules.Printing.API.Customizations.Dtos;

public record CustomizationResponse(
	Guid Id,
	decimal Infill,
	decimal Volume,
	decimal Base,
	decimal Scale,
	decimal Weight,
	decimal Cost,
	string Color,
	int MaterialId
);
