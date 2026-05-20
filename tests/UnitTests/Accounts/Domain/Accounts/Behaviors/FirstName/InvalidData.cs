namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName;

using static Data.Accounts.TestData;

public class InvalidData : TheoryData<string?>
{
	public InvalidData()
	{
		Add(MinInvalidFirstName);
		Add(MaxInvalidFirstName);
	}
}
