namespace CustomCADs.Printing.Application.Materials.Commands.Internal.SetTexture;

public sealed record SetMaterialTextureCommand(
	MaterialId Id,
	string? Key,
	string? ContentType
) : ICommand;
