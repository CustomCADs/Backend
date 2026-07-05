using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create.WithId;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		CreateCustomization();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		Customization customization = CreateCustomization(MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId, ValidId);

		using (Assert.Multiple())
		{
			await Assert.That(customization.Id).IsEqualTo(ValidId);
			await Assert.That(customization.Scale).IsEqualTo(MaxValidScale);
			await Assert.That(customization.Infill).IsEqualTo(MaxValidInfill);
			await Assert.That(customization.Volume).IsEqualTo(MaxValidVolume);
			await Assert.That(customization.Color).IsEqualTo(ValidColor);
			await Assert.That(customization.MaterialId).IsEqualTo(ValidMaterialId);
		}
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenInvalid(decimal scale, decimal infill, decimal volume, string color)
	{
		Assert.Throws<CustomValidationException<Customization>>(() => CreateCustomization(scale, infill, volume, color));
	}
}
