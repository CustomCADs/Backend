namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create;

using static Data.Customs.TestData;

public class ValidData : ITheoryData<(string, string, bool)>
{
	public static IEnumerable<(string, string, bool)> GetTestData()
	{
		yield return (MinValidName, MinValidDescription, true);
		yield return (MaxValidName, MaxValidDescription, false);
	}
}
