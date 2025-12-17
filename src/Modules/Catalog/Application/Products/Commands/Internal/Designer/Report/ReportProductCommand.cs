using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.Report;

public sealed record ReportProductCommand(
	ProductId Id,
	AccountId CallerId
) : ICommand;
