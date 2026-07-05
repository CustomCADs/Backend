namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

using static Data.Users.TestData;

public class ValidData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidUsername;
		yield return MaxValidUsername;
	}
}
