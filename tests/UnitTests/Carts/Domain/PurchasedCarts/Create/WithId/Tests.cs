
namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Create.WithId;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		CreateCart(ValidBuyerId, ValidId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		var cart = CreateCart(ValidBuyerId, ValidId);

		using (Assert.Multiple())
		{
			await Assert.That(cart.Id).IsEqualTo(ValidId);
			await Assert.That(cart.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(cart.Items).IsEmpty();
			await Assert.That(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1)).IsTrue();
		}
	}
}
