namespace CustomCADs.UnitTests.Files.Domain.Images.Create;

using static Data.Images.TestData;

public class InvalidData : ITheoryData<(string, string)>
{
	public static IEnumerable<(string, string)> GetTestData()
	{
		// Key
		yield return (InvalidKey, ValidContentType);

		// Content Type
		yield return (ValidKey, InvalidContentType);
	}
}
