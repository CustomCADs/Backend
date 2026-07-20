namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username;

using static Data.Accounts.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return ValidUsername;
		yield return MinValidUsername;
		yield return MaxValidUsername;
	}
}
