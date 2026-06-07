namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username;

using static Data.Accounts.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(ValidUsername);
		Add(MinValidUsername);
		Add(MaxValidUsername);
	}
}
