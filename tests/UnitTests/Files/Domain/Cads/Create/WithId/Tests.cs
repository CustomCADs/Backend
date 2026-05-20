using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.WithId;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenCadIsValid()
	{
		CreateCad();
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenCadIsValid()
	{
		var cad = CreateCad(ValidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords, id: ValidId);

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
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenCadIsInvalid(string key, string contentType, decimal volume, Coordinates camCoords, Coordinates panCoords)
	{
		Assert.Throws<CustomValidationException<Cad>>(
			() => CreateCad(key, contentType, volume, camCoords, panCoords)
		);
	}
}
