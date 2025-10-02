using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.Report;

public sealed record ReportProductCommand(
	ProductId Id,
	AccountId CallerId
) : ICommand;
