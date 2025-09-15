namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record DuplicateCadsByIdsCommand(
	CadId[] Ids
) : ICommand<Dictionary<CadId, CadId>>;
