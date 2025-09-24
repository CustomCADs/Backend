using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Get.All;

public class GetPurchasedCartsMapper : ResponseMapper<GetPurchasedCartsResponse, GetAllPurchasedCartsDto>
{
	public override GetPurchasedCartsResponse FromEntity(GetAllPurchasedCartsDto cart)
		=> new(
			Id: cart.Id.Value,
			Total: cart.Total,
			PurchasedAt: cart.PurchasedAt,
			ItemsCount: cart.ItemsCount
		);
}
