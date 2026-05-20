namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName;

using static Data.Accounts.TestData;

public class ValidData : TheoryData<string?>
{
	public ValidData()
	{
		Add(ValidFirstName);
		Add(ValidFirstNameNull);
	}
}
