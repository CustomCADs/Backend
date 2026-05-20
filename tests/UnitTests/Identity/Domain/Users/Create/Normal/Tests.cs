using CustomCADs.Modules.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.Normal;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private static readonly Email Email = new(ValidEmail, true);

	[Fact]
	public void Create_ShouldNotThrowException()
	{
		User.Create(ValidRole, MaxValidUsername, Email, ValidAccountId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		User user = User.Create(ValidRole, MaxValidUsername, Email, ValidAccountId);

		Assert.Multiple(
			() => Assert.Equal(ValidRole, user.Role),
			() => Assert.Equal(MaxValidUsername, user.Username),
			() => Assert.Equal(Email, user.Email),
			() => Assert.Equal(ValidAccountId, user.AccountId)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenUserInvalid(string role, string username, string email)
	{
		Assert.Throws<CustomValidationException<User>>(
			() => User.Create(role, username, Email with { Value = email }, ValidAccountId)
		);
	}
}
