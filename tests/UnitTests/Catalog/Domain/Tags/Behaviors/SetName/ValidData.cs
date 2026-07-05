namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

using static Data.Tags.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidName;
		yield return MaxValidName;
	}
}
