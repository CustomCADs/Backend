using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Delete;

public sealed record DeleteCustomCommand(
	CustomId Id,
	AccountId CallerId
) : ICommand;
