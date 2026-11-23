namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

using static Data.Accounts.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return ValidEmail1;
		yield return ValidEmail2;
		yield return ValidEmail3;
	}
}
