using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Queries.Get.All;

public sealed record GetPurchasedCartsRequest(
	PurchasedCartSortingType SortingType = PurchasedCartSortingType.PurchasedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	PaymentStatus? PaymentStatus = null,
	DateTimeOffset? Start = null,
	DateTimeOffset? End = null,
	bool? HasDelivery = null,
	int Page = 1,
	int Limit = 20
);
