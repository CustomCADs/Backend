namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Description;

using static Data.Categories.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return ValidDescription;
		yield return MinValidDescription;
		yield return MaxValidDescription;
	}
}
