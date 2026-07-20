namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Volume;

using static Data.Customizations.TestData;

public class TestData : ITheoryData<decimal>
{
	public static IEnumerable<decimal> GetTestData()
	{
		yield return MinInvalidVolume;
		yield return MaxInvalidVolume;
	}
}
