namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

using static Data.Products.TestData;

public class ValidData : ITheoryData<decimal>
{
	public static IEnumerable<decimal> GetTestData()
	{
		yield return MinValidPrice;
		yield return MaxValidPrice;
	}
}
