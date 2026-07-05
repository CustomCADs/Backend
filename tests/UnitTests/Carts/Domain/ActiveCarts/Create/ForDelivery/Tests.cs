
namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException_WhenCartIsValid()
	{
		CreateItemWithDelivery(
			buyerId: ValidBuyerId,
			productId: ValidProductId,
			customizationId: ValidCustomizationId
		);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		var item = CreateItemWithDelivery(
			buyerId: ValidBuyerId,
			productId: ValidProductId,
			customizationId: ValidCustomizationId
		);

		using (Assert.Multiple())
		{
			await Assert.That(item.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(ValidProductId).IsEqualTo(item.ProductId);
			await Assert.That(ValidCustomizationId).IsEqualTo(item.CustomizationId);
			await Assert.That(item.ForDelivery).IsTrue();
		}
	}
}
