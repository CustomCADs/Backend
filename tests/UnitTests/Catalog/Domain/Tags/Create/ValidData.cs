namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create;

using static Data.Tags.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidName;
		yield return MaxValidName;
	}
}
