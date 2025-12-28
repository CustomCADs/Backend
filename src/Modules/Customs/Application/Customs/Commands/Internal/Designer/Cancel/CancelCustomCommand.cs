using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Cancel;

public sealed record CancelCustomCommand(
	CustomId Id,
	AccountId CallerId
) : ICommand;
