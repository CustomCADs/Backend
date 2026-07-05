namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using static Data.Customs.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinInvalidName;
		yield return MaxInvalidName;
	}
}
