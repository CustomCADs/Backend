namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Create.Normal;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		PurchasedCart.Create(ValidBuyerId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		var cart = PurchasedCart.Create(ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(ValidBuyerId, cart.BuyerId),
			() => Assert.Empty(cart.Items),
			() => Assert.True(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1))
		);
	}
}
