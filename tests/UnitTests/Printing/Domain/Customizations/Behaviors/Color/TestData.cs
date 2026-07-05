namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Color;

using static Data.Customizations.TestData;

public class TestData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidColor;
		yield return MinInvalidColor;
		yield return MaxInvalidColor;
	}
}
