using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Removed;

public class ProductRemoveUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void Remove_ShouldNotThrowException_WhenStatusIsValid()
	{
		CreateProduct().Report().Remove();
	}

	[Fact]
	public void Remove_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Remove()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Validate().Remove()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report().Remove().Remove())
		);
	}
}
