using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetFirstName_ShouldNotThrowException_WhenFirstNameIsValid(string firstName)
	{
		var account = CreateAccount();

		account.SetFirstName(firstName);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetFirstName_SetsfirstName_WhenUsernameIsValid(string firstName)
	{
		var account = CreateAccount();

		account.SetFirstName(firstName);

		await Assert.That(firstName).IsEqualTo(account.FirstName);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetFirstName_ThrowsException_WhenUsernameIsInvalid(string firstName)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(() => account.SetFirstName(firstName));
	}
}
