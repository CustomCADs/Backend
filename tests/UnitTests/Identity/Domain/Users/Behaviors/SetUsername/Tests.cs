
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

public class Tests : Data.Users.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetUsername_ShouldNotThrowException(string username)
	{
		var user = CreateUser();

		user.SetUsername(username);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetUsername_PopulatesProperty(string username)
	{
		var user = CreateUser();

		user.SetUsername(username);

		await Assert.That(username).IsEqualTo(user.Username);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetUsername_ThrowsException_WhenUsernameIsInvalid(string username)
	{
		var user = CreateUser();

		Assert.Throws<CustomValidationException<User>>(() => user.SetUsername(username));
	}
}
