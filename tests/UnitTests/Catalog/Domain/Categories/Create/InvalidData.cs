namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create;

using static Data.Categories.TestData;

public class InvalidData : ITheoryData<(string, string)>
{
	public static IEnumerable<(string, string)> GetTestData()
	{
		// Name
		yield return (InvalidName, ValidDescription);
		yield return (MinInvalidName, MinValidDescription);
		yield return (MaxInvalidName, MaxValidDescription);

		// Description
		yield return (ValidName, InvalidDescription);
		yield return (MinValidName, MinInvalidDescription);
		yield return (MaxValidName, MaxInvalidDescription);
	}
}
