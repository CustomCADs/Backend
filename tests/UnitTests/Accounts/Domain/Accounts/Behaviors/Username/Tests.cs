using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Username;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetUsername_ShouldNotThrowException_WhenUsernameIsValid(string username)
	{
		var account = CreateAccount();

		account.SetUsername(username);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetUsername_SetsUsername_WhenUserameIsValid(string username)
	{
		var account = CreateAccount();

		account.SetUsername(username);

		await Assert.That(username).IsEqualTo(account.Username);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetUsername_ThrowsException_WhenUserameIsInvalid(string username)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(() => account.SetUsername(username));
	}
}
