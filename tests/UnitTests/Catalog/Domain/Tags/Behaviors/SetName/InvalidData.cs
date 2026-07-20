namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

using static Data.Tags.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinInvalidName;
		yield return MaxInvalidName;
	}
}
