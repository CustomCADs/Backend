
namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddPurchase;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	public void Add_ShouldNotThrowException()
	{
		var product = CreateProduct();
		product.AddToPurchaseCount();
	}

	[Test]
	[Arguments(1)]
	[Arguments(3)]
	[Arguments(5)]
	[Arguments(10)]
	public async Task Add_ShouldIncreasePurchaseCountResult(int iterations)
	{
		var product = CreateProduct();

		for (int i = 0; i < iterations; i++)
		{
			product.AddToPurchaseCount();
		}

		await Assert.That(product.Counts.Purchases).IsEqualTo(iterations);
	}
}