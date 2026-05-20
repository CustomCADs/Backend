namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username;

using static Data.Accounts.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidUsername);
		Add(MinInvalidUsername);
		Add(MaxInvalidUsername);
	}
}
