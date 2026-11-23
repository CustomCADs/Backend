namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

using static Data.Users.TestData;

public class InvalidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return InvalidUsername;
		yield return MinInvalidUsername;
		yield return MaxInvalidUsername;
	}
}
