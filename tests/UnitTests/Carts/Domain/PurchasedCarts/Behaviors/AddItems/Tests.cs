using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Behaviors.AddItems;

using static PurchasedCartConstants;
using static Data.PurchasedCarts.TestData.CartItemsData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Fact]
	public void AddItems_ShouldNotThrowException_WhenItemsCountIsValid()
	{
		CreateCart().AddItems([
			new(MaxValidPrice, ValidCadId, ValidProductId, false, null, 1, DateTimeOffset.UtcNow)
		]);
	}

	[Fact]
	public void AddItems_ShouldThrowException_WhenItemsCountIsNotValid()
	{
		var purchasedCart = CreateCart();
		for (int i = 0; i < ItemsCountMax; i++)
		{
			purchasedCart.AddItems([
				new(MaxValidPrice, ValidCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
			]);
		}

		Assert.Throws<CustomValidationException<PurchasedCart>>(
			() => purchasedCart.AddItems([
				new(MaxValidPrice, ValidCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
			])
		);
	}
}
