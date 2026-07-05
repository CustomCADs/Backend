using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetPrice_ShouldNotThrow_WhenPriceIsValid(decimal price)
	{
		CreateProduct().SetPrice(price);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetPrice_ShouldPopulateProperties_WhenPriceIsValid(decimal price)
	{
		var product = CreateProduct();
		product.SetPrice(price);
		await Assert.That(product.Price).IsEqualTo(price);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetPrice_ShouldThrowException_WhenPriceIsNotValid(decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetPrice(price));
	}
}
