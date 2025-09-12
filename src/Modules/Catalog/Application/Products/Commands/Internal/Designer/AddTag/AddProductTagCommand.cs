using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.AddTag;

public record AddProductTagCommand(
	ProductId Id,
	TagId TagId,
	AccountId CallerId
) : ICommand;
