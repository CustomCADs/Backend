using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldNotThrow_WhenNameIsValid(string name)
	{
		CreateProduct().SetName(name);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetName_ShouldPopulateProperties_WhenNameIsValid(string name)
	{
		var product = CreateProduct();
		product.SetName(name);
		await Assert.That(product.Name).IsEqualTo(name);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldThrowException_WhenNameIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetName(name));
	}
}
