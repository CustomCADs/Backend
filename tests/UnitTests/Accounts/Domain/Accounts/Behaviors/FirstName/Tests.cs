using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetFirstName_ShouldNotThrowException_WhenFirstNameIsValid(string firstName)
	{
		var account = CreateAccount();

		account.SetFirstName(firstName);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetFirstName_SetsfirstName_WhenUsernameIsValid(string firstName)
	{
		var account = CreateAccount();

		account.SetFirstName(firstName);

		Assert.Equal(account.FirstName, firstName);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetFirstName_ThrowsException_WhenUsernameIsInvalid(string firstName)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(
			() => account.SetFirstName(firstName)
		);
	}
}
