namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record DuplicateCadsByIdsCommand(
	CadId[] Ids,
	AccountId CallerId
) : ICommand<Dictionary<CadId, CadId>>;
