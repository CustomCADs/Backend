namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

using static Data.Accounts.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidEmail);
		Add(InvalidEmailLocal);
		Add(InvalidEmailDomain);
		Add(InvalidEmailTLD);
		Add(InvalidEmailTLDMin);
	}
}
