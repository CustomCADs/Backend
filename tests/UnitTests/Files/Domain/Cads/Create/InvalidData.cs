using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create;

using static Data.Cads.TestData;

public class InvalidData : TheoryData<string, string, decimal, Coordinates, Coordinates>
{
	public InvalidData()
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
