namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

using static Data.Customs.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinInvalidDescription;
		yield return MaxInvalidDescription;
	}
}
