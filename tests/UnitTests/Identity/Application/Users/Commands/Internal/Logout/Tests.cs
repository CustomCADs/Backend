using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Logout;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Logout;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly LogoutUserHandler handler;
	private readonly LogoutUserCommand request = new("refresh-token");

	private readonly Mock<IUserService> service = new();

	private static readonly RefreshToken Token = RefreshToken.Create("refresh-token", ValidFingerprint, ValidId, longerSession: false);
	private readonly User user = CreateUser(username: MaxValidUsername);

	public Tests()
	{
		handler = new(service.Object);

		service.Setup(x => x.GetByRefreshTokenAsync(Token.Value)).ReturnsAsync((user, Token));
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.RevokeRefreshTokenAsync(Token.Value),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenMissingToken()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request with { RefreshToken = null }, ct));
	}
}