namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.SetDescription;

using static Data.Notifications.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
