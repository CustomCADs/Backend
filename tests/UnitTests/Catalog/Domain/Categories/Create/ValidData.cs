namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create;

using static Data.Categories.TestData;

public class ValidData : ITheoryData<(string, string)>
{
	public static IEnumerable<(string, string)> GetTestData()
	{
		yield return (ValidName, ValidDescription);
		yield return (MinValidName, MinValidDescription);
		yield return (MaxValidName, MaxValidDescription);
	}
}
