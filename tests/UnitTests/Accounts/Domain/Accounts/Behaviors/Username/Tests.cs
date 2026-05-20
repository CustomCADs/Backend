using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetUsername_ShouldNotThrowException_WhenUsernameIsValid(string username)
	{
		var account = CreateAccount();

		account.SetUsername(username);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetUsername_SetsUsername_WhenUserameIsValid(string username)
	{
		var account = CreateAccount();

		account.SetUsername(username);

		Assert.Equal(account.Username, username);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetUsername_ThrowsException_WhenUserameIsInvalid(string username)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(
			() => account.SetUsername(username)
		);
	}
}
