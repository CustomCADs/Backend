namespace CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Delete;

public sealed record DeleteMaterialCommand(
	MaterialId Id
) : ICommand;
