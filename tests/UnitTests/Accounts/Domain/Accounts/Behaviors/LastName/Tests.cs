using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetLastName_ShouldNotThrowException_WhenLastNameIsValid(string lastName)
	{
		var account = CreateAccount();

		account.SetLastName(lastName);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetLastName_SetslastName_WhenUsernameIsValid(string lastName)
	{
		var account = CreateAccount();

		account.SetLastName(lastName);

		Assert.Equal(account.LastName, lastName);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetLastName_ThrowsException_WhenUsernameIsInvalid(string lastName)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(
			() => account.SetLastName(lastName)
		);
	}
}
