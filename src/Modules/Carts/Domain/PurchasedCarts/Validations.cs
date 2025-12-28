namespace CustomCADs.Modules.Carts.Domain.PurchasedCarts;

using static PurchasedCartConstants;

internal static class Validations
{
	extension(PurchasedCart cart)
	{
		internal PurchasedCart ValidateItems()
			=> cart
				.ThrowIfInvalidSize(
					expression: x => x.Items.ToArray(),
					size: (ItemsCountMin, ItemsCountMax),
					property: nameof(cart.Items)
				);
	}

}
