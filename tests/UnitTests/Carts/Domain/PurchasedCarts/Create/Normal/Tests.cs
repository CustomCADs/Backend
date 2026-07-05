
namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Create.Normal;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		PurchasedCart.Create(ValidBuyerId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		var cart = PurchasedCart.Create(ValidBuyerId);

		using (Assert.Multiple())
		{
			await Assert.That(cart.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(cart.Items).IsEmpty();
			await Assert.That(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1)).IsTrue();
		}
	}
}
