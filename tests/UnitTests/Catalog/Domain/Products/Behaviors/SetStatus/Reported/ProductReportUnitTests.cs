using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Reported;

public class ProductReportUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void Report_ShouldNotThrowException_WhenStatusIsValid()
	{
		Assert.Multiple(
			() => CreateProduct().Report(),
			() => CreateProduct().Validate().Report()
		);
	}

	[Fact]
	public void Report_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report().Report()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report().Remove().Report())
		);
	}
}
