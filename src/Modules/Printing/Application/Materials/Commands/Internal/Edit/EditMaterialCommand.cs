namespace CustomCADs.Printing.Application.Materials.Commands.Internal.Edit;

public sealed record EditMaterialCommand(
	MaterialId Id,
	string Name,
	decimal Density,
	decimal Cost
) : ICommand;
