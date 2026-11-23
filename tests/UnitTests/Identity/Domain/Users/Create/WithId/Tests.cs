using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.WithId;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		CreateUser();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		User user = CreateUser(ValidRole, MaxValidUsername, ValidEmail, ValidAccountId, id: ValidId);

		using (Assert.Multiple())
		{
			await Assert.That(user.Id).IsEqualTo(ValidId);
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
		Assert.Throws<CustomValidationException<User>>(() => CreateUser(role, username, email, ValidAccountId, id: ValidId));
	}
}
