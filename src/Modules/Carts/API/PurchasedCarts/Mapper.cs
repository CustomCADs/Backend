namespace CustomCADs.Carts.API.PurchasedCarts;

internal static class Mapper
{
	internal static PurchasedCartItemResponse ToResponse(this PurchasedCartItemDto item)
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
