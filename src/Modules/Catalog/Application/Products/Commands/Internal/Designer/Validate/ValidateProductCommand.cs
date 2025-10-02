using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.Validate;

public sealed record ValidateProductCommand(
	ProductId Id,
	AccountId CallerId
) : ICommand;
