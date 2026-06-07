using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.WithId;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateUser();
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		User user = CreateUser(ValidRole, MaxValidUsername, ValidEmail, ValidAccountId, id: ValidId);

		Assert.Multiple(
			() => Assert.Equal(ValidRole, user.Role),
			() => Assert.Equal(MaxValidUsername, user.Username),
			() => Assert.Equal(ValidEmail, user.Email.Value),
			() => Assert.Equal(ValidAccountId, user.AccountId)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenUserInvalid(string role, string username, string email)
	{
		Assert.Throws<CustomValidationException<User>>(
			() => CreateUser(role, username, email, ValidAccountId, id: ValidId)
		);
	}
}
