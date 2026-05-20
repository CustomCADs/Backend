using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Color;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Fact]
	public void SetColor_ShouldNotThrowException()
	{
		CreateCustomization().SetColor(ValidColor);
	}

	[Fact]
	public void SetColor_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetColor(ValidColor);

		Assert.Equal(ValidColor, material.Color);
	}

	[Theory]
	[ClassData(typeof(TestData))]
	public void SetColor_ShouldThrowException_WhenColorInvalid(string color)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetColor(color)
		);
	}
}
