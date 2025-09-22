namespace CustomCADs.Printing.API.Customizations.Endpoints.Post;

public record CreateCustomizationRequest(
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	int MaterialId
);
