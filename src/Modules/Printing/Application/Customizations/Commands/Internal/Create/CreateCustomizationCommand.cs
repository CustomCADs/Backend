namespace CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Create;

public sealed record CreateCustomizationCommand(
	decimal Infill,
	decimal Volume,
	decimal Base,
	decimal Scale,
	string Color,
	MaterialId MaterialId
) : ICommand<CustomizationId>;
