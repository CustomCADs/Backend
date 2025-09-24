using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Get.Single;

public class GetPurchasedCartMappper : ResponseMapper<GetPurchasedCartResponse, GetPurchasedCartByIdDto>
{
	public override GetPurchasedCartResponse FromEntity(GetPurchasedCartByIdDto cart)
		=> new(
			Id: cart.Id.Value,
			Total: cart.Total,
			PurchasedAt: cart.PurchasedAt,
			PaymentStatus: cart.PaymentStatus,
			BuyerName: cart.BuyerName,
			ShipmentId: cart.ShipmentId?.Value,
			Items: [.. cart.Items.Select(x => x.ToResponse())]
		);
}
