namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using static Data.Customs.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidName;
		yield return MaxValidName;
	}
}
