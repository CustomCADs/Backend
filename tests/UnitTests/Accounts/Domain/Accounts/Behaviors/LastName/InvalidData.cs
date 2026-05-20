namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName;

using static Data.Accounts.TestData;

public class InvalidData : TheoryData<string?>
{
	public InvalidData()
	{
		Add(MinInvalidLastName);
		Add(MaxInvalidLastName);
	}
}
