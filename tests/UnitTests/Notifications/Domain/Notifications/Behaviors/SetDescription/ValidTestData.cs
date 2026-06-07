namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription;

using static Data.Notifications.TestData;

public class ValidTestData : TheoryData<string>
{
	public ValidTestData()
	{
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
