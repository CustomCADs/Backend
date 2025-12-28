using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Create;

public sealed record CreateMaterialCommand(
	string Name,
	decimal Density,
	decimal Cost,
	ImageId TextureId
) : ICommand<MaterialId>;
