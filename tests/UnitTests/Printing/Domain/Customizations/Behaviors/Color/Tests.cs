using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Color;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void SetColor_ShouldNotThrowException()
	{
		CreateCustomization().SetColor(ValidColor);
	}

	[Test]
	public async Task SetColor_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetColor(ValidColor);

		await Assert.That(material.Color).IsEqualTo(ValidColor);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetColor_ShouldThrowException_WhenColorInvalid(string color)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(() => material.SetColor(color));
	}
}
