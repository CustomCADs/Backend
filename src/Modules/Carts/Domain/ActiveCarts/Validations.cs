namespace CustomCADs.Modules.Carts.Domain.ActiveCarts;

using static ActiveCartItemConstants;

internal static class Validations
{
	extension(ActiveCartItem item)
	{
		internal ActiveCartItem ValidateQuantity()
			=> item
				.ThrowIfInvalidRange(
					expression: x => x.Quantity,
					range: (QuantityMin, QuantityMax)
				);
	}

}
