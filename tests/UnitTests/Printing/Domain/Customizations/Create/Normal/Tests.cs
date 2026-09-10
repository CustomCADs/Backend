using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create.Normal;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		Customization.Create(MaxValidInfill, new(Value: MaxValidVolume, Base: MaxValidBase, Scale: MaxValidScale), ValidColor, ValidMaterialId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		Customization customization = Customization.Create(MaxValidInfill, new(Value: MaxValidVolume, Base: MaxValidBase, Scale: MaxValidScale), ValidColor, ValidMaterialId);

		using (Assert.Multiple())
		{
			await Assert.That(customization.Infill).IsEqualTo(MaxValidInfill);
			await Assert.That(customization.Volume.Value).IsEqualTo(MaxValidVolume);
			await Assert.That(customization.Volume.Base).IsEqualTo(MaxValidBase);
			await Assert.That(customization.Volume.Scale).IsEqualTo(MaxValidScale);
			await Assert.That(customization.Color).IsEqualTo(ValidColor);
			await Assert.That(customization.MaterialId).IsEqualTo(ValidMaterialId);
		}
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenInvalid(decimal scale, decimal infill, decimal @base, decimal volume, string color)
	{
		Assert.Throws<CustomValidationException<Customization>>(() => Customization.Create(infill, new(volume, @base, scale), color, ValidMaterialId));
	}
}
