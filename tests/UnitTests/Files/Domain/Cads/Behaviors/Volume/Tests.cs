using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.Volume;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	[Test]
	public void SetKey_ShouldNotThrowException_WhenKeyIsValid()
	{
		var cad = CreateCad();

		cad.SetVolume(ValidVolume);
	}

	[Test]
	public async Task SetKey_ShouldPopulateProperties_WhenKeyIsValid()
	{
		var cad = CreateCad();

		cad.SetVolume(ValidVolume);

		await Assert.That(cad.Volume).IsEqualTo(ValidVolume);
	}

	[Test]
	public void SetKey_ShouldThrowException_WhenKeyIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(() => cad.SetVolume(InvalidVolume));
	}
}
