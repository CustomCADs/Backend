using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Admin.Remove;

public sealed record RemoveProductCommand(
	ProductId Id,
	AccountId CallerId
) : ICommand;
