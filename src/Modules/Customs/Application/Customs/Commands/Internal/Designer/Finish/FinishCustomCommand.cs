using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;

public sealed record FinishCustomCommand(
	CustomId Id,
	decimal Price,
	CadId CadId,
	AccountId CallerId
) : ICommand;
