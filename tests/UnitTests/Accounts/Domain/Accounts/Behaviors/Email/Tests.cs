using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetEmail_ShouldNotThrowException_WhenEmailIsValid(string email)
	{
		var account = CreateAccount();

		account.SetEmail(email);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetEmail_Setsemail_WhenUserameIsValid(string email)
	{
		var account = CreateAccount();

		account.SetEmail(email);

		Assert.Equal(account.Email, email);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetEmail_ThrowsException_WhenUserameIsInvalid(string email)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(
			() => account.SetEmail(email)
		);
	}
}
