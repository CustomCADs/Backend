namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create;

using static Data.Roles.TestData;

public class ValidData : ITheoryData<(string, string)>
{
	public static IEnumerable<(string, string)> GetTestData()
	{
		yield return (ValidName, ValidDescription);
		yield return (MinValidName, MinValidDescription);
		yield return (MaxValidName, MaxValidDescription);
	}
}
