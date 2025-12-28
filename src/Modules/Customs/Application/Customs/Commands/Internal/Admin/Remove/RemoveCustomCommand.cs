using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Admin.Remove;

public record RemoveCustomCommand(
	CustomId Id,
	AccountId CallerId
) : ICommand;
