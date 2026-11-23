namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Queries.Get.Stats;

public sealed record PurchasedCartsStatsResponse(
	int Total,
	Dictionary<Guid, int> Counts
);
