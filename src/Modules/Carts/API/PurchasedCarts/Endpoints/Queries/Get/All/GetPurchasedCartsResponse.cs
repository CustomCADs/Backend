namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Queries.Get.All;

public sealed record GetPurchasedCartsResponse(
	Guid Id,
	decimal Total,
	DateTimeOffset PurchasedAt,
	int ItemsCount
);
