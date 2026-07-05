namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetPrice;

using static Data.Products.TestData;

public class InvalidData : ITheoryData<decimal>
{
	public static IEnumerable<decimal> GetTestData()
	{
		yield return MinInvalidPrice;
		yield return MaxInvalidPrice;
	}
}
