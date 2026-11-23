using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		CreateAccount(role, username, email, createdAt: null, firstName, lastName);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		var account = CreateAccount(role, username, email, createdAt: null, firstName, lastName, ValidId);


		using (Assert.Multiple())
		{
			await Assert.That(account.Id).IsEqualTo(ValidId);
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
		Assert.Throws<CustomValidationException<Account>>(() => CreateAccount(role, username, email, createdAt: null, firstName, lastName));
	}
}
