using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Unchecked;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	public void Uncheck_ShouldNotThrowException_WhenStatusIsValid()
	{
		using (Assert.Multiple())
		{
			CreateProduct().Validate(ValidDesignerId).Uncheck();
			CreateProduct().Report(ValidDesignerId).Uncheck();
		}
	}

	[Test]
	public void Uncheck_ShouldThrowException_WhenStatusIsNotValid()
	{
		using (Assert.Multiple())
		{
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Uncheck());
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Remove().Uncheck());
		}
	}
}
