using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		Account.Create(role, username, email, firstName, lastName);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		var account = Account.Create(role, username, email, firstName, lastName);

		Assert.Multiple(
			() => Assert.Equal(role, account.RoleName),
			() => Assert.Equal(username, account.Username),
			() => Assert.Equal(email, account.Email),
			() => Assert.Equal(firstName, account.FirstName),
			() => Assert.Equal(lastName, account.LastName)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenAccountIsInvalid(string role, string username, string email, string? firstName, string? lastName)
	{
		Assert.Throws<CustomValidationException<Account>>(
			() => Account.Create(role, username, email, firstName, lastName)
		);
	}
}
