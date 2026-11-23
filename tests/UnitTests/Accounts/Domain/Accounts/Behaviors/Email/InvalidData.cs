namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

using static Data.Accounts.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidEmail;
		yield return InvalidEmailLocal;
		yield return InvalidEmailDomain;
		yield return InvalidEmailTLD;
		yield return InvalidEmailTLDMin;
	}
}
