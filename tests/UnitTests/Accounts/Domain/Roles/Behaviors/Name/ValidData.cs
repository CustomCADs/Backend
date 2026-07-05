namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

using static Data.Roles.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return ValidName;
		yield return MinValidName;
		yield return MaxValidName;
	}
}
