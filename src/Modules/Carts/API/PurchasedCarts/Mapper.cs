namespace CustomCADs.Modules.Carts.API.PurchasedCarts;

internal static class Mapper
{
	extension(PurchasedCartItemDto item)
	{
		internal PurchasedCartItemResponse ToResponse()
			=> new(
				Quantity: item.Quantity,
				ForDelivery: item.ForDelivery,
				Price: item.Price,
				Cost: item.Cost,
				AddedAt: item.AddedAt,
				ProductId: item.ProductId.Value,
				CartId: item.CartId.Value,
				CadId: item.CadId.Value,
				CustomizationId: item.CustomizationId?.Value
			);
	}

}
