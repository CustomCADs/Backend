using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Unchecked;

public class ProductUncheckUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void Uncheck_ShouldNotThrowException_WhenStatusIsValid()
	{
		Assert.Multiple(
			() => CreateProduct().Validate().Uncheck(),
			() => CreateProduct().Report().Uncheck()
		);
	}

	[Fact]
	public void Uncheck_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Uncheck()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report().Remove().Uncheck())
		);
	}
}
