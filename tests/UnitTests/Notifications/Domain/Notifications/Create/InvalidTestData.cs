namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Create;

using static Data.Notifications.TestData;

public class InvalidData : TheoryData<string, string>
{
	public InvalidData()
	{
		// Name
		Add(MinInvalidType, MinValidDescription);
		Add(MaxInvalidType, MaxValidDescription);

		// Description
		Add(MinValidType, MinInvalidDescription);
		Add(MaxValidType, MaxInvalidDescription);
	}
}
