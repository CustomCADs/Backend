namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription;

using static Data.Notifications.TestData;

public class ValidTestData : ITheoryData<string>
{
	public static IEnumerable<string> GetTestData()
	{
		yield return MinValidDescription;
		yield return MaxValidDescription;
	}
}
