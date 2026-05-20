using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Create.WithId;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name, string description, decimal price)
	{
		CreateProduct(
			id: ValidId,
			name: name,
			description: description,
			price: price
		);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties_WhenProductIsValid(string name, string description, decimal price)
	{
		Product product = CreateProduct(
			id: ValidId,
			name: name,
			description: description,
			price: price
		);

		Assert.Multiple(
			() => Assert.Equal(name, product.Name),
			() => Assert.Equal(description, product.Description),
			() => Assert.Equal(price, product.Price),
			() => Assert.Equal(ProductStatus.Unchecked, product.Status)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name, string description, decimal price)
	{
		Assert.Throws<CustomValidationException<Product>>(
			() => CreateProduct(
				id: ValidId,
				name: name,
				description: description,
				price: price
			)
		);
	}
}
