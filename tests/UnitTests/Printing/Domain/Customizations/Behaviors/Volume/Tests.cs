using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void SetVolume_ShouldNotThrowException()
	{
		CreateCustomization().SetVolume(MaxValidVolume);
	}

	[Test]
	public async Task SetVolume_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetVolume(MaxValidVolume);

		await Assert.That(material.Volume).IsEqualTo(MaxValidVolume);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetVolume_ShouldThrowException_WhenVolumeInvalid(decimal volume)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(() => material.SetVolume(volume));
	}
}
