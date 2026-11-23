using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		CreateCad();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WhenCadIsValid()
	{
		var cad = CreateCad(ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords, id: ValidId);

		using (Assert.Multiple())
		{
			await Assert.That(cad.Id).IsEqualTo(ValidId);
			await Assert.That(cad.Key).IsEqualTo(ValidKey);
			await Assert.That(cad.ContentType).IsEqualTo(ValidContentType);
			await Assert.That(cad.Volume).IsEqualTo(ValidVolume);
			await Assert.That(cad.CamCoordinates).IsEqualTo(ValidCoords);
			await Assert.That(cad.PanCoordinates).IsEqualTo(ValidCoords);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenCadIsInvalid(string key, string contentType, decimal volume, Coordinates camCoords, Coordinates panCoords)
	{
		Assert.Throws<CustomValidationException<Cad>>(() => CreateCad(key, contentType, volume, camCoords, panCoords));
	}
}
