namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create;

using static Data.Roles.TestData;

public class ValidData : TheoryData<string, string>
{
	public ValidData()
	{
		Add(ValidName, ValidDescription);
		Add(MinValidName, MinValidDescription);
		Add(MaxValidName, MaxValidDescription);
	}
}
