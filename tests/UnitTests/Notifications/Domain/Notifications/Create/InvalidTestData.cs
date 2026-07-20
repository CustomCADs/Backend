namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create;

using static Data.Notifications.TestData;

public class InvalidData : ITheoryData<(string, string)>
{
	public static IEnumerable<(string, string)> GetTestData()
	{
		// Name
		yield return (MinInvalidType, MinValidDescription);
		yield return (MaxInvalidType, MaxValidDescription);

		// Description
		yield return (MinValidType, MinInvalidDescription);
		yield return (MaxValidType, MaxInvalidDescription);
	}
}
