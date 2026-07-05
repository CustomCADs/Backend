namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

using static Data.Categories.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidName;
		yield return MinInvalidName;
		yield return MaxInvalidName;
	}
}
