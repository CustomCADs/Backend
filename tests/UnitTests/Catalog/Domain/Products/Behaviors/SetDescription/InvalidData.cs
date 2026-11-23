namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

using static Data.Products.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinInvalidDescription;
		yield return MaxInvalidDescription;
	}
}
