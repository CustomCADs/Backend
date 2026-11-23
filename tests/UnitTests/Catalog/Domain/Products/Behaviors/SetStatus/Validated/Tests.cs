using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Validated;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Test]
	public void Validate_ShouldNotThrowException_WhenStatusIsValid()
	{
		CreateProduct().Validate(ValidDesignerId);
	}

	[Test]
	public void Validate_ShouldThrowException_WhenStatusIsNotValid()
	{
		using (Assert.Multiple())
		{
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Validate(ValidDesignerId).Validate(ValidDesignerId));
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Validate(ValidDesignerId));
			Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Remove().Validate(ValidDesignerId));
		}
	}
}
