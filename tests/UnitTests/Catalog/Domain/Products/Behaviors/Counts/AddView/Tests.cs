
namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddView;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	public void Add_ShouldNotThrowException()
	{
		var product = CreateProduct();
		product.AddToViewCount();
	}

	[Test]
	[Arguments(1)]
	[Arguments(3)]
	[Arguments(5)]
	[Arguments(10)]
	public async Task Add_ShouldIncreaseViewCountResult(int iterations)
	{
		var product = CreateProduct();

		for (int i = 0; i < iterations; i++)
		{
			product.AddToViewCount();
		}

		await Assert.That(product.Counts.Views).IsEqualTo(iterations);
	}
}