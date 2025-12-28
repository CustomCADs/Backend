using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Creator.Delete;

public sealed record DeleteProductCommand(
	ProductId Id,
	AccountId CallerId
) : ICommand;
