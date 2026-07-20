using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create.Normal;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		Customization.Create(MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		Customization customization = Customization.Create(MaxValidScale, MaxValidInfill, MaxValidVolume, ValidColor, ValidMaterialId);

		using (Assert.Multiple())
		{
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
		Assert.Throws<CustomValidationException<Customization>>(() => Customization.Create(scale, infill, volume, color, ValidMaterialId));
	}
}
