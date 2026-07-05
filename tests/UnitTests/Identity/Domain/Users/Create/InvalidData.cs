namespace CustomCADs.UnitTests.Identity.Domain.Users.Create;

using static Data.Users.TestData;

public class InvalidData : ITheoryData<(string, string, string)>
{
	public static IEnumerable<(string, string, string)> GetTestData()
	{
		// Role
		yield return (InvalidRole, MaxValidUsername, ValidEmail);

		// Username
		yield return (ValidRole, InvalidUsername, ValidEmail);
		yield return (ValidRole, MaxInvalidUsername, ValidEmail);
		yield return (ValidRole, MinInvalidUsername, ValidEmail);

		// Email
		yield return (ValidRole, MaxValidUsername, InvalidEmail);
	}
}
