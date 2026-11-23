using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Login;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Login;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly LoginUserHandler handler;
	private readonly LoginUserCommand request = new(
		Username: MaxValidUsername,
		Password: MinValidPassword,
		LongerExpireTime: false,
		Fingerprint: ValidFingerprint
	);

	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();

	private readonly User user = CreateUser(username: MaxValidUsername);
	private static readonly RefreshToken RefreshToken = RefreshToken.Create("refresh-token", ValidFingerprint, ValidId, false);
	private static readonly TokensDto Tokens = new(
		Role: "role",
		AccessToken: new("access-token", DateTimeOffset.UtcNow),
		RefreshToken: new("refresh-token", DateTimeOffset.UtcNow),
		CsrfToken: new("csrf-token", DateTimeOffset.UtcNow)
	);

	public Tests()
	{
		handler = new(service.Object, tokenService.Object);

		tokenService.Setup(x => x.IssueRefreshToken(
			It.IsAny<Func<string, RefreshToken>>())
		).Returns((Func<string, RefreshToken> factory) => factory(RefreshToken.Value));
		tokenService.Setup(x => x.IssueTokens(user, It.Is<RefreshToken>(x => x.Value == RefreshToken.Value))).Returns(Tokens);

		service.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
		service.Setup(x => x.CheckPasswordAsync(user.Username, MinValidPassword)).ReturnsAsync(true);
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetByUsernameAsync(MaxValidUsername),
			Times.Once()
		);
		service.Verify(
			x => x.GetIsLockedOutAsync(MaxValidUsername),
			Times.Once()
		);
		service.Verify(
			x => x.CheckPasswordAsync(user.Username, MinValidPassword),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldIssueTokens()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		tokenService.Verify(
			x => x.IssueRefreshToken(
				It.IsAny<Func<string, RefreshToken>>()
			),
			Times.Once()
		);
		tokenService.Verify(
			x => x.IssueTokens(user, It.Is<RefreshToken>(x => x.Value == RefreshToken.Value)),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		TokensDto tokens = await handler.Handle(request, ct);

		// Assert
		await Assert.That(tokens).IsEqualTo(Tokens);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenPasswordIncorrect()
	{
		// Arrange
		service.Setup(x => x.CheckPasswordAsync(user.Username, MinValidPassword)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUserLockedOut()
	{
		// Arrange
		service.Setup(x => x.GetIsLockedOutAsync(user.Username)).ReturnsAsync(DateTimeOffset.UtcNow);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUserNotVerified()
	{
		// Arrange
		User unverifiedUser = CreateUser(email: ValidEmail, isVerified: false);
		service.Setup(x => x.GetByUsernameAsync(unverifiedUser.Username)).ReturnsAsync(unverifiedUser);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request with { Username = unverifiedUser.Username }, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		service.Setup(x => x.GetByUsernameAsync(request.Username))
			.ThrowsAsync(new Exception($"{request.Username} does not exist."));

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request, ct));
	}
}
