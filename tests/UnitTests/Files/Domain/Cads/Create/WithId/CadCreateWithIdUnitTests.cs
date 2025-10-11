using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId;

using static CadsData;

public class CadCreateWithIdUnitTests : CadsBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		CreateCadWithId(ValidId, ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords);
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties_WhenCadIsValid()
	{
		var cad = CreateCadWithId(ValidId, ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords);

		Assert.Multiple(
			() => Assert.Equal(ValidId, cad.Id),
			() => Assert.Equal(ValidKey, cad.Key),
			() => Assert.Equal(ValidContentType, cad.ContentType),
			() => Assert.Equal(ValidVolume, cad.Volume),
			() => Assert.Equal(ValidCoords, cad.CamCoordinates),
			() => Assert.Equal(ValidCoords, cad.PanCoordinates)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CadCreateInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenCadIsInvalid(string key, string contentType, decimal volume, Coordinates camCoords, Coordinates panCoords)
	{
		Assert.Throws<CustomValidationException<Cad>>(
			() => CreateCadWithId(ValidId, key, contentType, volume, camCoords, panCoords)
		);
	}
}
