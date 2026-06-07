namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

using static Data.Roles.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(ValidName);
		Add(MinValidName);
		Add(MaxValidName);
	}
}
