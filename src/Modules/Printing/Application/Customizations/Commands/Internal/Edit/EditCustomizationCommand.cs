namespace CustomCADs.Printing.Application.Customizations.Commands.Internal.Edit;

public sealed record EditCustomizationCommand(
	CustomizationId Id,
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	MaterialId MaterialId
) : ICommand;
