using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Removed;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	public void Remove_ShouldNotThrowException_WhenStatusIsValid()
	{
		CreateProduct().Report(ValidDesignerId).Remove();
	}

	[Test]
	public void Remove_ShouldThrowException_WhenStatusIsNotValid()
	{
		using (Assert.Multiple())
		{
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Remove());
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Validate(ValidDesignerId).Remove());
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Remove().Remove());
		}
	}
}
