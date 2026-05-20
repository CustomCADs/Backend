using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

public class Tests : Data.Products.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldNotThrow_WhenNameIsValid(string name)
	{
		CreateProduct().SetName(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldPopulateProperties_WhenNameIsValid(string name)
	{
		var product = CreateProduct();
		product.SetName(name);
		Assert.Equal(name, product.Name);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetName_ShouldThrowException_WhenNameIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct().SetName(name)
		);
	}
}
