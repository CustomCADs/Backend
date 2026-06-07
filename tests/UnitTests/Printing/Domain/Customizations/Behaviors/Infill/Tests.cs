using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Infill;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Fact]
	public void SetInfill_ShouldNotThrowException()
	{
		CreateCustomization().SetInfill(MaxValidInfill);
	}

	[Fact]
	public void SetInfill_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetInfill(MaxValidInfill);

		Assert.Equal(MaxValidInfill, material.Infill);
	}

	[Theory]
	[ClassData(typeof(TestData))]
	public void SetInfill_ShouldThrowException_WhenInfillInvalid(decimal infill)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetInfill(infill)
		);
	}
}
