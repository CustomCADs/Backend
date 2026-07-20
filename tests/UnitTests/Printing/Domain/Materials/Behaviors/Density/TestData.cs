namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Density;

using static Data.Materials.TestData;

public class TestData : ITheoryData<decimal>
{
	public static IEnumerable<decimal> GetTestData()
	{
		yield return MinInvalidDensity;
		yield return MaxInvalidDensity;
	}
}
