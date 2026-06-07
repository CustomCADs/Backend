namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

using static Data.Accounts.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(ValidEmail1);
		Add(ValidEmail2);
		Add(ValidEmail3);
	}
}
