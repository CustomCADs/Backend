namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDescription;

using static Data.Products.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidDescription;
		yield return MaxValidDescription;
	}
}
