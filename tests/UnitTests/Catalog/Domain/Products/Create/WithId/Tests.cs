using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name, string description, decimal price)
	{
		CreateProduct(
			id: ValidId,
			name: name,
			description: description,
			price: price
		);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties_WhenProductIsValid(string name, string description, decimal price)
	{
		Product product = CreateProduct(
			id: ValidId,
			name: name,
			description: description,
			price: price
		);

		using (Assert.Multiple())
		{
			await Assert.That(product.Id).IsEqualTo(ValidId);
			await Assert.That(product.Name).IsEqualTo(name);
			await Assert.That(product.Description).IsEqualTo(description);
			await Assert.That(product.Price).IsEqualTo(price);
			await Assert.That(product.Status).IsEqualTo(ProductStatus.Unchecked);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name, string description, decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(() => CreateProduct(
				id: ValidId,
				name: name,
				description: description,
				price: price
			));
	}
}
