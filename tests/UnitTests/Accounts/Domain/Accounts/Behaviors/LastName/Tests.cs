using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetLastName_ShouldNotThrowException_WhenLastNameIsValid(string lastName)
	{
		var account = CreateAccount();

		account.SetLastName(lastName);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetLastName_SetslastName_WhenUsernameIsValid(string lastName)
	{
		var account = CreateAccount();

		account.SetLastName(lastName);

		await Assert.That(lastName).IsEqualTo(account.LastName);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetLastName_ThrowsException_WhenUsernameIsInvalid(string lastName)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(() => account.SetLastName(lastName));
	}
}
