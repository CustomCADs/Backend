namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName;

using static Data.Accounts.TestData;

public class ValidData : ITheoryData<string?>
{
	public static IEnumerable<string?> GetTestData()
	{
		yield return ValidLastName;
		yield return ValidLastNameNull;
	}
}
