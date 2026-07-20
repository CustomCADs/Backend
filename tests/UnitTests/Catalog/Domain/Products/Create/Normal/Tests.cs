using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.Normal;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name, string description, decimal price)
	{
		Product.Create(
			name: name,
			description: description,
			price: price,
			creatorId: ValidCreatorId,
			categoryId: ValidCategoryId,
			imageId: ValidImageId,
			cadId: ValidCadId
		);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties_WhenProductIsValid(string name, string description, decimal price)
	{
		Product product = Product.Create(
			name: name,
			description: description,
			price: price,
			creatorId: ValidCreatorId,
			categoryId: ValidCategoryId,
			imageId: ValidImageId,
			cadId: ValidCadId
		);

		using (Assert.Multiple())
		{
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
		Assert.Throws<CustomValidationException<Product>>(() => Product.Create(
				name: name,
				description: description,
				price: price,
				creatorId: ValidCreatorId,
				categoryId: ValidCategoryId,
				imageId: ValidImageId,
				cadId: ValidCadId
			));
	}
}
