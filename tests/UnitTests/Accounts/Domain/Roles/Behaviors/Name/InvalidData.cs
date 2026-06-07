namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

using static Data.Roles.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidName);
		Add(MinInvalidName);
		Add(MaxInvalidName);
	}
}
