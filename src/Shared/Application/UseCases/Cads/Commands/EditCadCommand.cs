namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record EditCadCommand(
	CadId Id,
	string ContentType,
	decimal Volume
) : ICommand;
