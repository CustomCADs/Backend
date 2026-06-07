namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

using static Data.Roles.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidDescription);
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
