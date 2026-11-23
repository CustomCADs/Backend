namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

using static Data.Roles.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return ValidDescription;
		yield return MinValidDescription;
		yield return MaxValidDescription;
	}
}
