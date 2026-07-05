using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Create;

using static Data.Cads.TestData;

public class InvalidData : ITheoryData<(string, string, decimal, Coordinates, Coordinates)>
{
	public static IEnumerable<(string, string, decimal, Coordinates, Coordinates)> GetTestData()
	{

		// Key
		yield return (InvalidKey, ValidContentType, ValidVolume, ValidCoords, ValidCoords);

		// Content Type
		yield return (ValidKey, InvalidContentType, ValidVolume, ValidCoords, ValidCoords);

		// Volume
		yield return (ValidKey, ValidContentType, InvalidVolume, ValidCoords, ValidCoords);

		// CamCoordinates
		yield return (ValidKey, ValidContentType, ValidVolume, MaxInvalidCoords, ValidCoords);
		yield return (ValidKey, ValidContentType, ValidVolume, MinInvalidCoords, ValidCoords);

		// CamCoordinates
		yield return (ValidKey, ValidContentType, ValidVolume, ValidCoords, MaxInvalidCoords);
		yield return (ValidKey, ValidContentType, ValidVolume, ValidCoords, MinInvalidCoords);
	}
}
