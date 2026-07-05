namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

using static Data.Roles.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidDescription;
		yield return MinInvalidDescription;
		yield return MaxInvalidDescription;
	}
}
