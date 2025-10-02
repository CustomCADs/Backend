using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Unchecked;

using static ProductsData;

public class ProductUncheckUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void Uncheck_ShouldNotThrowException_WhenStatusIsValid()
	{
		Assert.Multiple(
			() => CreateProduct().Validate(ValidDesignerId).Uncheck(),
			() => CreateProduct().Report(ValidDesignerId).Uncheck()
		);
	}

	[Fact]
	public void Uncheck_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Uncheck()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Remove().Uncheck())
		);
	}
}
