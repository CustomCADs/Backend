using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Validated;

public class ProductValidateUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void Validate_ShouldNotThrowException_WhenStatusIsValid()
	{
		CreateProduct().Validate();
	}

	[Fact]
	public void Validate_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Validate().Validate()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report().Validate()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report().Remove().Validate())
		);
	}
}
