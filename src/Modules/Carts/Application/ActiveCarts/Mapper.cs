namespace CustomCADs.Modules.Carts.Application.ActiveCarts;

internal static class Mapper
{
	extension(ActiveCartItem item)
	{
		internal ActiveCartItemDto ToDto(string buyer)
			=> new(
				Quantity: item.Quantity,
				ForDelivery: item.ForDelivery,
				BuyerName: buyer,
				AddedAt: item.AddedAt,
				BuyerId: item.BuyerId,
				ProductId: item.ProductId,
				CustomizationId: item.CustomizationId
			);
	}

}
