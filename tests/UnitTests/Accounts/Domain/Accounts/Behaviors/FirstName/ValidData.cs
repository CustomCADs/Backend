namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName;

using static Data.Accounts.TestData;

public class ValidData : ITheoryData<string?>
{
	public static IEnumerable<string?> GetTestData()
	{
		yield return ValidFirstName;
		yield return ValidFirstNameNull;
	}
}
