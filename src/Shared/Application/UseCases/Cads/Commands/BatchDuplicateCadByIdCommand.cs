namespace CustomCADs.Shared.Application.UseCases.Cads.Commands;

public sealed record BatchDuplicateCadByIdCommand(
	CadId[] Ids,
	AccountId CallerId
) : ICommand<Dictionary<CadId, CadId>>;
