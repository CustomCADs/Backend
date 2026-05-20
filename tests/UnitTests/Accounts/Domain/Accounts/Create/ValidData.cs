namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

using static Data.Accounts.TestData;
using RolesData = Data.Roles.TestData;

public class ValidData : TheoryData<string, string, string, string?, string?>
{
	public ValidData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
