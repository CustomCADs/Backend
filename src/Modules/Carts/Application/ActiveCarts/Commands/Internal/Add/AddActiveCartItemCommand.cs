using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Add;

public sealed record AddActiveCartItemCommand(
	AccountId CallerId,
	bool ForDelivery,
	ProductId ProductId,
	CustomizationId? CustomizationId
) : ICommand;
