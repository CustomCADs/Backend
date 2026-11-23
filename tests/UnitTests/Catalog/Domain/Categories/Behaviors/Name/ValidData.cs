namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

using static Data.Categories.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return ValidName;
		yield return MinValidName;
		yield return MaxValidName;
	}
}
