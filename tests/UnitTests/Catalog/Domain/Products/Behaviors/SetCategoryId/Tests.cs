
namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetCategoryId;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	public void SetCategoryId_ShouldNotThrowException()
	{
		CreateProduct().SetCategoryId(ValidCategoryId);
	}

	[Test]
	public async Task SetCategoryId_ShouldPopulateProperties()
	{
		var product = CreateProduct();
		product.SetCategoryId(ValidCategoryId);
		await Assert.That(product.CategoryId).IsEqualTo(ValidCategoryId);
	}
}
