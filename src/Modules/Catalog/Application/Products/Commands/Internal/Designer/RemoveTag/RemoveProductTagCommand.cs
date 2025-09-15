using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;

public sealed record RemoveProductTagCommand(
	ProductId Id,
	TagId TagId,
	AccountId CallerId
) : ICommand;
