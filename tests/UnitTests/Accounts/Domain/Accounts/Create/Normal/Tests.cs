using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		Account.Create(role, username, email, firstName, lastName);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		var account = Account.Create(role, username, email, firstName, lastName);

		using (Assert.Multiple())
		{
			await Assert.That(account.RoleName).IsEqualTo(role);
			await Assert.That(account.Username).IsEqualTo(username);
			await Assert.That(account.Email).IsEqualTo(email);
			await Assert.That(account.FirstName).IsEqualTo(firstName);
			await Assert.That(account.LastName).IsEqualTo(lastName);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenAccountIsInvalid(string role, string username, string email, string? firstName, string? lastName)
	{
		Assert.Throws<CustomValidationException<Account>>(() => Account.Create(role, username, email, firstName, lastName));
	}
}
