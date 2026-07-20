using static CustomCADs.UnitTests.Accounts.Data.Accounts.TestData;
using RolesData = CustomCADs.UnitTests.Accounts.Data.Roles.TestData;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

public class ValidData : ITheoryData<(string, string, string, string?, string?)>
{
	public static IEnumerable<(string, string, string, string?, string?)> GetTestData()
	{
		yield return (RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		yield return (RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		yield return (RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
