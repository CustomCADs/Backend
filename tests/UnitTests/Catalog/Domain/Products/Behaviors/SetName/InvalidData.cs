namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

using static Data.Products.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinInvalidName;
		yield return MaxInvalidName;
	}
}
