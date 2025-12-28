using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

public sealed record ToggleActiveCartItemForDeliveryCommand(
	AccountId CallerId,
	ProductId ProductId,
	CustomizationId? CustomizationId
) : ICommand;
