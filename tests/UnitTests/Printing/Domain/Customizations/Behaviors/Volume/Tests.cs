using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Fact]
	public void SetVolume_ShouldNotThrowException()
	{
		CreateCustomization().SetVolume(MaxValidVolume);
	}

	[Fact]
	public void SetVolume_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetVolume(MaxValidVolume);

		Assert.Equal(MaxValidVolume, material.Volume);
	}

	[Theory]
	[ClassData(typeof(TestData))]
	public void SetVolume_ShouldThrowException_WhenVolumeInvalid(decimal volume)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(
			() => material.SetVolume(volume)
		);
	}
}
