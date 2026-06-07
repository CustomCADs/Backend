namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

using static Data.Users.TestData;

public class InvalidData : TheoryData<string>
{
	public InvalidData()
	{
		Add(InvalidUsername);
		Add(MinInvalidUsername);
		Add(MaxInvalidUsername);
	}
}
