using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void SetVolume_ShouldNotThrowException()
	{
		CreateCustomization().SetVolume(MaxValidVolume, MaxValidBase, MaxValidScale);
	}

	[Test]
	public async Task SetVolume_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetVolume(MaxValidVolume, MaxValidBase, MaxValidScale);

		using (Assert.Multiple())
		{
			await Assert.That(material.Volume.Value).IsEqualTo(MaxValidVolume);
			await Assert.That(material.Volume.Scale).IsEqualTo(MaxValidScale);
			await Assert.That(material.Volume.Base).IsEqualTo(MaxValidBase);
		}
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetVolume_ShouldThrowException_WhenVolumeInvalid(decimal volume, decimal @base, decimal scale)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(() => material.SetVolume(volume, @base, scale));
	}
}
