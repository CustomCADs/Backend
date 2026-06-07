namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Create.WithId;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateCart(ValidBuyerId, ValidId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		var cart = CreateCart(ValidBuyerId, ValidId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, cart.Id),
			() => Assert.Equal(ValidBuyerId, cart.BuyerId),
			() => Assert.Empty(cart.Items),
			() => Assert.True(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1))
		);
	}
}
