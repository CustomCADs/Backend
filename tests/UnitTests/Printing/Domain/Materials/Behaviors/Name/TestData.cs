namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Name;

using static Data.Materials.TestData;

public class TestData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidName;
		yield return MinInvalidName;
		yield return MaxInvalidName;
	}
}
