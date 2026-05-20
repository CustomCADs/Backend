using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

public class Tests : Data.Products.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetPrice_ShouldNotThrow_WhenPriceIsValid(decimal price)
	{
		CreateProduct().SetPrice(price);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetPrice_ShouldPopulateProperties_WhenPriceIsValid(decimal price)
	{
		var product = CreateProduct();
		product.SetPrice(price);
		Assert.Equal(price, product.Price);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetPrice_ShouldThrowException_WhenPriceIsNotValid(decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct().SetPrice(price)
		);
	}
}
