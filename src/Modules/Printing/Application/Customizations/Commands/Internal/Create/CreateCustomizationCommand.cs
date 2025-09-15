namespace CustomCADs.Printing.Application.Customizations.Commands.Internal.Create;

public sealed record CreateCustomizationCommand(
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	MaterialId MaterialId
) : ICommand<CustomizationId>;
