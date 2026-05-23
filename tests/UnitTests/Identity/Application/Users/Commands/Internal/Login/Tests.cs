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

	private readonly User User = CreateUser(username: MaxValidUsername);
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
		).Returns(RefreshToken);
		tokenService.Setup(x => x.IssueTokens(User, RefreshToken)).Returns(Tokens);

		service.Setup(x => x.GetByUsernameAsync(User.Username)).ReturnsAsync(User);
		service.Setup(x => x.CheckPasswordAsync(User.Username, MinValidPassword)).ReturnsAsync(true);
	}

	[Fact]
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
			x => x.CheckPasswordAsync(User.Username, MinValidPassword),
			Times.Once()
		);
	}

	[Fact]
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
			x => x.IssueTokens(User, RefreshToken),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		TokensDto tokens = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Tokens, tokens);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenPasswordIncorrect()
	{
		// Arrange
		service.Setup(x => x.CheckPasswordAsync(User.Username, MinValidPassword)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserLockedOut()
	{
		// Arrange
		service.Setup(x => x.GetIsLockedOutAsync(User.Username)).ReturnsAsync(DateTimeOffset.UtcNow);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotVerified()
	{
		// Arrange
		User unverifiedUser = CreateUser(email: ValidEmail, isVerified: false);
		service.Setup(x => x.GetByUsernameAsync(unverifiedUser.Username)).ReturnsAsync(unverifiedUser);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			() => handler.Handle(request with { Username = unverifiedUser.Username }, ct)
		);
	}
}
