namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username;

using static Data.Accounts.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidUsername;
		yield return MinInvalidUsername;
		yield return MaxInvalidUsername;
	}
}
