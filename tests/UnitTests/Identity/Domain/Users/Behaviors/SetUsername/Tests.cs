
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

public class Tests : Data.Users.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetUsername_ShouldNotThrowException(string username)
	{
		var user = CreateUser();

		user.SetUsername(username);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetUsername_PopulatesProperty(string username)
	{
		var user = CreateUser();

		user.SetUsername(username);

		Assert.Equal(user.Username, username);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetUsername_ThrowsException_WhenUsernameIsInvalid(string username)
	{
		var user = CreateUser();

		Assert.Throws<CustomValidationException<User>>(
			() => user.SetUsername(username)
		);
	}
}
