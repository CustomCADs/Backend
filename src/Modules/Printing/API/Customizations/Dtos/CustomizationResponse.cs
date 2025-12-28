namespace CustomCADs.Modules.Printing.API.Customizations.Dtos;

public record CustomizationResponse(
	Guid Id,
	decimal Scale,
	decimal Infill,
	decimal Weight,
	decimal Cost,
	string Color,
	int MaterialId
);
