using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Report;

public sealed record ReportCustomCommand(
	CustomId Id,
	AccountId CallerId
) : ICommand;
