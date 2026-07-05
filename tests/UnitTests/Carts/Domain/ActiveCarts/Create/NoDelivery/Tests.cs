
namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.NoDelivery;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException_WhenCartIsValid()
	{
		CreateItem(
			buyerId: ValidBuyerId,
			productId: ValidProductId
		);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		var item = CreateItem(
			buyerId: ValidBuyerId,
			productId: ValidProductId
		);

		using (Assert.Multiple())
		{
			await Assert.That(item.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(ValidProductId).IsEqualTo(item.ProductId);
			await Assert.That(item.ForDelivery).IsFalse();
		}
	}
}
