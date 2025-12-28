using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

public sealed record GetPurchasedCartByIdDto(
	PurchasedCartId Id,
	decimal Total,
	DateTimeOffset PurchasedAt,
	PaymentStatus PaymentStatus,
	string BuyerName,
	ShipmentId? ShipmentId,
	ICollection<PurchasedCartItemDto> Items
);
