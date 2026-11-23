namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

using static Data.Roles.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidName;
		yield return MinInvalidName;
		yield return MaxInvalidName;
	}
}
