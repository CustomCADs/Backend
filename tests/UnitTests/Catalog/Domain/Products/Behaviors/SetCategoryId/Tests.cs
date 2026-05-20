namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetCategoryId;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Fact]
	public void SetCategoryId_ShouldNotThrowException()
	{
		CreateProduct().SetCategoryId(ValidCategoryId);
	}

	[Fact]
	public void SetCategoryId_ShouldPopulateProperties()
	{
		var product = CreateProduct();
		product.SetCategoryId(ValidCategoryId);
		Assert.Equal(ValidCategoryId, product.CategoryId);
	}
}
