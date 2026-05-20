namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.SetNoDelivery;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Fact]
	public void SetForDelivery_ShouldNotThrowException()
	{
		CreateItem().SetNoDelivery();
	}

	[Fact]
	public void SetForDelivery_ShouldPopulateProperties()
	{
		var item = CreateItem();
		item.SetNoDelivery();
		Assert.False(item.ForDelivery);
	}
}
