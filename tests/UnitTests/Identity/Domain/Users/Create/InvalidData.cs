namespace CustomCADs.UnitTests.Identity.Domain.Users.Create;

using static Data.Users.TestData;

public class InvalidData : TheoryData<string, string, string>
{
	public InvalidData()
	{
		// Role
		Add(InvalidRole, MaxValidUsername, ValidEmail);

		// Username
		Add(ValidRole, InvalidUsername, ValidEmail);
		Add(ValidRole, MaxInvalidUsername, ValidEmail);
		Add(ValidRole, MinInvalidUsername, ValidEmail);

		// Email
		Add(ValidRole, MaxValidUsername, InvalidEmail);
	}
}
