namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

using static Data.Roles.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(ValidDescription);
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
