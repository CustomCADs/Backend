namespace CustomCADs.Modules.Printing.API.Customizations.Endpoints.Mutations.Put;

public record EditCustomizationRequest(
	Guid Id,
	decimal Infill,
	decimal Volume,
	decimal Base,
	decimal Scale,
	string Color,
	int MaterialId
);
