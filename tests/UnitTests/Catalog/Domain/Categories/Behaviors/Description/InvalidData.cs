namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Description;

using static Data.Categories.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidDescription;
		yield return MinInvalidDescription;
		yield return MaxInvalidDescription;
	}
}
