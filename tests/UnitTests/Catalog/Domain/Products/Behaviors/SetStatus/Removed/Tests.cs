using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Removed;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Fact]
	public void Remove_ShouldNotThrowException_WhenStatusIsValid()
	{
		CreateProduct().Report(ValidDesignerId).Remove();
	}

	[Fact]
	public void Remove_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Remove()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Validate(ValidDesignerId).Remove()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Remove().Remove())
		);
	}
}
