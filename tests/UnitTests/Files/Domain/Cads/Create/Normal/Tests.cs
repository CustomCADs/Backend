using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		Cad.Create(ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords, ValidOwnerId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WhenCadIsValid()
	{
		var cad = Cad.Create(ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords, ValidOwnerId);

		using (Assert.Multiple())
		{
			await Assert.That(cad.Key).IsEqualTo(ValidKey);
			await Assert.That(cad.ContentType).IsEqualTo(ValidContentType);
			await Assert.That(cad.Volume).IsEqualTo(ValidVolume);
			await Assert.That(cad.CamCoordinates).IsEqualTo(ValidCoords);
			await Assert.That(cad.PanCoordinates).IsEqualTo(ValidCoords);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType, decimal volume, Coordinates camCoords, Coordinates panCoords)
	{
		Assert.Throws<CustomValidationException<Cad>>(() => Cad.Create(key, contentType, volume, camCoords, panCoords, ValidOwnerId));
	}
}
