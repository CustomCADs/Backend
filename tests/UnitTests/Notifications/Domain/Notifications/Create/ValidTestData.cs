namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create;

using static Data.Notifications.TestData;

public class ValidData : TheoryData<string, string>
{
	public ValidData()
	{
		Add(MinValidType, MinValidDescription);
		Add(MaxValidType, MaxValidDescription);
	}
}
