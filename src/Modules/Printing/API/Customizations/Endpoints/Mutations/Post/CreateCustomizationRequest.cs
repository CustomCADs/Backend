namespace CustomCADs.Modules.Printing.API.Customizations.Endpoints.Mutations.Post;

public record CreateCustomizationRequest(
	decimal Infill,
	decimal Volume,
	decimal Base,
	decimal Scale,
	string Color,
	int MaterialId
);
