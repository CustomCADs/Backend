namespace CustomCADs.Modules.Carts.Domain.PurchasedCarts.Entities;

using static PurchasedCartConstants.PurchasedCartItems;

internal static class Validations
{
	extension(PurchasedCartItem item)
	{
		internal PurchasedCartItem ValidateQuantity()
			=> item
				.ThrowIfInvalidRange(
					expression: x => x.Quantity,
					range: (QuantityMin, QuantityMax)
				);

		internal PurchasedCartItem ValidatePrice()
			=> item
				.ThrowIfInvalidRange(
					expression: x => x.Price,
					range: (PriceMin, PriceMax)
				);
	}

}
