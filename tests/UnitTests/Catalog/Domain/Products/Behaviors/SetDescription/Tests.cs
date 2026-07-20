using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldNotThrow_WhenDescriptionIsValid(string description)
	{
		CreateProduct().SetDescription(description);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetDescription_ShouldPopulateProperties_WhenDescriptionIsValid(string description)
	{
		var product = CreateProduct();
		product.SetDescription(description);
		await Assert.That(product.Description).IsEqualTo(description);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionIsNotValid(string description)
	{
		Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetDescription(description));
	}
}
