using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Reported;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	[Fact]
	public void Report_ShouldNotThrowException_WhenStatusIsValid()
	{
		Assert.Multiple(
			() => CreateProduct().Report(ValidDesignerId),
			() => CreateProduct().Validate(ValidDesignerId).Report(ValidDesignerId)
		);
	}

	[Fact]
	public void Report_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Report(ValidDesignerId)),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().Report(ValidDesignerId).Remove().Report(ValidDesignerId))
		);
	}
}
