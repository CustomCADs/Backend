namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

using static Data.Accounts.TestData;
using RolesData = Data.Roles.TestData;

public class InvalidData : ITheoryData<(string, string, string, string?, string?)>
{
	public static IEnumerable<(string, string, string, string?, string?)> GetTestData()
	{
		// Role
		yield return (RolesData.InvalidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		yield return (RolesData.MinInvalidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		yield return (RolesData.MaxInvalidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);

		// Username
		yield return (RolesData.ValidName, InvalidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		yield return (RolesData.MinValidName, MinInvalidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		yield return (RolesData.MaxValidName, MaxInvalidUsername, ValidEmail3, ValidFirstName, ValidLastName);

		// First Name
		yield return (RolesData.ValidName, ValidUsername, ValidEmail1, MinInvalidFirstName, ValidLastName);
		yield return (RolesData.MinValidName, MinValidUsername, ValidEmail2, MaxInvalidFirstName, ValidLastNameNull);

		// Last Name
		yield return (RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, MinInvalidLastName);
		yield return (RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, MaxInvalidLastName);

		// Email
		yield return (RolesData.ValidName, ValidUsername, InvalidEmail, ValidFirstName, ValidLastName);
		yield return (RolesData.MinValidName, MinValidUsername, InvalidEmailLocal, ValidFirstNameNull, ValidLastNameNull);
		yield return (RolesData.MaxValidName, MaxValidUsername, InvalidEmailDomain, ValidFirstName, ValidLastName);
	}
}
