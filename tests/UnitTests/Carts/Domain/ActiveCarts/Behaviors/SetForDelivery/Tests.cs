
namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.SetForDelivery;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Test]
	public void SetForDelivery_ShouldNotThrowException()
	{
		CreateItem().SetForDelivery(ValidCustomizationId);
	}

	[Test]
	public async Task SetForDelivery_ShouldPopulateProperties()
	{
		var item = CreateItem();
		item.SetForDelivery(ValidCustomizationId);
		using (Assert.Multiple())
		{
			await Assert.That(item.ForDelivery).IsTrue();
			await Assert.That(item.CustomizationId).IsEqualTo(ValidCustomizationId);
		}
	}
}
