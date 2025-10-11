using CustomCADs.Files.Domain.Cads.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create.Data;

using static CadsData;

public class CadCreateInvalidData : TheoryData<string, string, decimal, Coordinates, Coordinates>
{
	public CadCreateInvalidData()
	{

		// Key
		Add(InvalidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords);

		// Content Type
		Add(ValidKey, InvalidContentType, ValidVolume, ValidCoords, ValidCoords);

		// Volume
		Add(ValidKey, ValidContentType, InvalidVolume, ValidCoords, ValidCoords);

		// CamCoordinates
		Add(ValidKey, ValidContentType, ValidVolume, MaxInvalidCoords, ValidCoords);
		Add(ValidKey, ValidContentType, ValidVolume, MinInvalidCoords, ValidCoords);

		// CamCoordinates
		Add(ValidKey, ValidContentType, ValidVolume, ValidCoords, MaxInvalidCoords);
		Add(ValidKey, ValidContentType, ValidVolume, ValidCoords, MinInvalidCoords);
	}
}
