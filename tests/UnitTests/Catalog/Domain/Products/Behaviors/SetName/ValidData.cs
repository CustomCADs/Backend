namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetName;

using static Data.Products.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidName;
		yield return MaxValidName;
	}
}
