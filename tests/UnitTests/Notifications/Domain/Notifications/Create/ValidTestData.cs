namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create;

using static Data.Notifications.TestData;

public class ValidData : ITheoryData<(string, string)>
{
	public static IEnumerable<(string, string)> GetTestData()
	{
		yield return (MinValidType, MinValidDescription);
		yield return (MaxValidType, MaxValidDescription);
	}
}
