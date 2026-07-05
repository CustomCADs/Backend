using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Infill;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void SetInfill_ShouldNotThrowException()
	{
		CreateCustomization().SetInfill(MaxValidInfill);
	}

	[Test]
	public async Task SetInfill_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetInfill(MaxValidInfill);

		await Assert.That(material.Infill).IsEqualTo(MaxValidInfill);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetInfill_ShouldThrowException_WhenInfillInvalid(decimal infill)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(() => material.SetInfill(infill));
	}
}
