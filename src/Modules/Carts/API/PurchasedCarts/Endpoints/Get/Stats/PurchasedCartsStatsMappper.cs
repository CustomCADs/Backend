using CustomCADs.Shared.Domain.TypedIds.Carts;

namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Get.Stats;

using CountPurchasedCartsDto = (int TotalCartCount, Dictionary<PurchasedCartId, int> Counts);

public class PurchasedCartsStatsMappper : ResponseMapper<PurchasedCartsStatsResponse, CountPurchasedCartsDto>
{
	public override PurchasedCartsStatsResponse FromEntity(CountPurchasedCartsDto count)
		=> new(
			Total: count.TotalCartCount,
			Counts: count.Counts.ToDictionary(kv => kv.Key.Value, kv => kv.Value)
		);
}
