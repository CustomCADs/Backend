using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Entities;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts;

internal static class Mapper
{
	extension(PurchasedCart cart)
	{
		internal GetAllPurchasedCartsDto ToGetAllDto()
			=> new(
				Id: cart.Id,
				Total: cart.TotalCost,
				PurchasedAt: cart.PurchasedAt,
				ItemsCount: cart.Items.Count
			);

		internal GetPurchasedCartByIdDto ToGetByIdDto(string buyer)
			=> new(
				Id: cart.Id,
				Total: cart.TotalCost,
				PaymentStatus: cart.PaymentStatus,
				PurchasedAt: cart.PurchasedAt,
				BuyerName: buyer,
				ShipmentId: cart.ShipmentId,
				Items: [.. cart.Items.Select(i => i.ToDto())]
			);
	}

	extension(PurchasedCartItem item)
	{
		internal PurchasedCartItemDto ToDto()
			=> new(
				Quantity: item.Quantity,
				ForDelivery: item.ForDelivery,
				Price: item.Price,
				Cost: item.Cost,
				AddedAt: item.AddedAt,
				ProductId: item.ProductId,
				CartId: item.CartId,
				CadId: item.CadId,
				CustomizationId: item.CustomizationId
			);
	}

}
