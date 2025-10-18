using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Normal;

using static CadsData;

public class CadCreateUnitTests : CadsBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		CreateCad(ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords);
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenCadIsValid()
	{
		var cad = CreateCad(ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords);

		Assert.Multiple(
			() => Assert.Equal(ValidKey, cad.Key),
			() => Assert.Equal(ValidContentType, cad.ContentType),
			() => Assert.Equal(ValidVolume, cad.Volume),
			() => Assert.Equal(ValidCoords, cad.CamCoordinates),
			() => Assert.Equal(ValidCoords, cad.PanCoordinates)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CadCreateInvalidData))]
	public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType, decimal volume, Coordinates camCoords, Coordinates panCoords)
	{
		Assert.Throws<CustomValidationException<Cad>>(
			() => CreateCad(key, contentType, volume, camCoords, panCoords)
		);
	}
}
