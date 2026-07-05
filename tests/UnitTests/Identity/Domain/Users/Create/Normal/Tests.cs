using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.Normal;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private static readonly Email Email = new(ValidEmail, true);

	[Test]
	public void Create_ShouldNotThrowException()
	{
		User.Create(ValidRole, MaxValidUsername, Email, ValidAccountId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		User user = User.Create(ValidRole, MaxValidUsername, Email, ValidAccountId);

		using (Assert.Multiple())
		{
			await Assert.That(user.Role).IsEqualTo(ValidRole);
			await Assert.That(user.Username).IsEqualTo(MaxValidUsername);
			await Assert.That(user.Email.Value).IsEqualTo(ValidEmail);
			await Assert.That(user.AccountId).IsEqualTo(ValidAccountId);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenUserInvalid(string role, string username, string email)
	{
		Assert.Throws<CustomValidationException<User>>(() => User.Create(role, username, Email with { Value = email }, ValidAccountId));
	}
}
