namespace CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Edit;

public sealed record EditCustomizationCommand(
	CustomizationId Id,
	decimal Infill,
	decimal Volume,
	decimal Base,
	decimal Scale,
	string Color,
	MaterialId MaterialId
) : ICommand;
