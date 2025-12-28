using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Remove;

public sealed record RemoveActiveCartItemCommand(
	AccountId CallerId,
	ProductId ProductId
) : ICommand;
