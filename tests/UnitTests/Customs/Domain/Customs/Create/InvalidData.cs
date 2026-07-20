namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create;

using static Data.Customs.TestData;

public class InvalidData : ITheoryData<(string, string, bool)>
{
	public static IEnumerable<(string, string, bool)> GetTestData()
	{
		// Name
		yield return (MinInvalidName, MinValidDescription, true);
		yield return (MaxInvalidName, MaxValidDescription, false);

		// Description
		yield return (MinValidName, MinInvalidDescription, true);
		yield return (MaxValidName, MaxInvalidDescription, false);
	}
}
