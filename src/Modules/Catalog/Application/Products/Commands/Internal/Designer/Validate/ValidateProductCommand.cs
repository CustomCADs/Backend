using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.Validate;

public sealed record ValidateProductCommand(
	ProductId Id,
	AccountId CallerId
) : ICommand;
