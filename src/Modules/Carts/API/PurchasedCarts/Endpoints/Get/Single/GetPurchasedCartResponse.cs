using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Get.Single;

public sealed record GetPurchasedCartResponse(
	Guid Id,
	decimal Total,
	DateTimeOffset PurchasedAt,
	PaymentStatus PaymentStatus,
	string BuyerName,
	Guid? ShipmentId,
	ICollection<PurchasedCartItemResponse> Items
);
