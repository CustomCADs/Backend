namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

using static Data.Accounts.TestData;
using RolesData = Data.Roles.TestData;

public class InvalidData : TheoryData<string, string, string, string?, string?>
{
	public InvalidData()
	{
		// Role
		Add(RolesData.InvalidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinInvalidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxInvalidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);

		// Username
		Add(RolesData.ValidName, InvalidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinInvalidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxInvalidUsername, ValidEmail3, ValidFirstName, ValidLastName);

		// First Name
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, MinInvalidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, MaxInvalidFirstName, ValidLastNameNull);

		// Last Name
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, MinInvalidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, MaxInvalidLastName);

		// Email
		Add(RolesData.ValidName, ValidUsername, InvalidEmail, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, InvalidEmailLocal, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailDomain, ValidFirstName, ValidLastName);
	}
}
