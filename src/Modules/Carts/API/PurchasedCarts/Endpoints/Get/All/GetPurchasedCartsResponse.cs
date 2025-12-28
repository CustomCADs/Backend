namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Get.All;

public sealed record GetPurchasedCartsResponse(
	Guid Id,
	decimal Total,
	DateTimeOffset PurchasedAt,
	int ItemsCount
);
