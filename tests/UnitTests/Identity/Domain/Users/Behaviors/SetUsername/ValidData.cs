namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

using static Data.Users.TestData;

public class ValidData : TheoryData<string>
{
	public ValidData()
	{
		Add(MinValidUsername);
		Add(MaxValidUsername);
	}
}
