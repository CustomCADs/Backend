namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException_WhenCartIsValid()
	{
		CreateItemWithDelivery(
			buyerId: ValidBuyerId,
			productId: ValidProductId,
			customizationId: ValidCustomizationId
		);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		var item = CreateItemWithDelivery(
			buyerId: ValidBuyerId,
			productId: ValidProductId,
			customizationId: ValidCustomizationId
		);

		Assert.Multiple(
			() => Assert.Equal(ValidBuyerId, item.BuyerId),
			() => Assert.Equal(ValidProductId, item.ProductId),
			() => Assert.Equal(ValidCustomizationId, item.CustomizationId),
			() => Assert.True(item.ForDelivery)
		);
	}
}
