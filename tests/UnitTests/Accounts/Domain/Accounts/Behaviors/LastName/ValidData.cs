namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName;

using static Data.Accounts.TestData;

public class ValidData : TheoryData<string?>
{
	public ValidData()
	{
		Add(ValidLastName);
		Add(ValidLastNameNull);
	}
}
