
namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.SetNoDelivery;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Test]
	public void SetForDelivery_ShouldNotThrowException()
	{
		CreateItem().SetNoDelivery();
	}

	[Test]
	public async Task SetForDelivery_ShouldPopulateProperties()
	{
		var item = CreateItem();
		item.SetNoDelivery();
		await Assert.That(item.ForDelivery).IsFalse();
	}
}
