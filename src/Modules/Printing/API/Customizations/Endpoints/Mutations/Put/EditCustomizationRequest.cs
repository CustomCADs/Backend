namespace CustomCADs.Modules.Printing.API.Customizations.Endpoints.Mutations.Put;

public record EditCustomizationRequest(
	Guid Id,
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	int MaterialId
);
