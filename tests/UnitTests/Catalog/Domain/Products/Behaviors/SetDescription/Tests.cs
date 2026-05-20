using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

public class Tests : Data.Products.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_ShouldNotThrow_WhenDescriptionIsValid(string description)
	{
		CreateProduct().SetDescription(description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_ShouldPopulateProperties_WhenDescriptionIsValid(string description)
	{
		var product = CreateProduct();
		product.SetDescription(description);
		Assert.Equal(description, product.Description);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionIsNotValid(string description)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct().SetDescription(description)
		);
	}
}
