namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Cost;

using static Data.Materials.TestData;

public class TestData : ITheoryData<decimal>
{
	public static IEnumerable<decimal> GetTestData()
	{
		yield return MinInvalidCost;
		yield return MaxInvalidCost;
	}
}
