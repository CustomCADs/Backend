namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

using static Data.Customs.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidDescription;
		yield return MaxValidDescription;
	}
}
