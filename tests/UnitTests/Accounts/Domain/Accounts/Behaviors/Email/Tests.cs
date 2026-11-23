using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetEmail_ShouldNotThrowException_WhenEmailIsValid(string email)
	{
		var account = CreateAccount();

		account.SetEmail(email);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetEmail_Setsemail_WhenUserameIsValid(string email)
	{
		var account = CreateAccount();

		account.SetEmail(email);

		await Assert.That(email).IsEqualTo(account.Email);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetEmail_ThrowsException_WhenUserameIsInvalid(string email)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(() => account.SetEmail(email));
	}
}
