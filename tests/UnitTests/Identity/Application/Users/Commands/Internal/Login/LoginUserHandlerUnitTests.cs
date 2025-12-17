using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Login;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Login;

using static UsersData;

public class LoginUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly LoginUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();

	private readonly User User = CreateUser(username: MaxValidUsername);
	private static readonly RefreshToken RefreshToken = RefreshToken.Create("refresh-token", ValidId, false);
	private static readonly TokensDto Tokens = new(
		Role: "role",
		AccessToken: new("access-token", DateTimeOffset.UtcNow),
		RefreshToken: new("refresh-token", DateTimeOffset.UtcNow),
		CsrfToken: new("csrf-token", DateTimeOffset.UtcNow)
	);

	public LoginUserHandlerUnitTests()
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
		LoginUserCommand command = new(
			Username: User.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
		service.Verify(x => x.GetIsLockedOutAsync(MaxValidUsername), Times.Once());
		service.Verify(x => x.CheckPasswordAsync(User.Username, MinValidPassword), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldIssueTokens()
	{
		// Arrange
		LoginUserCommand command = new(
			Username: User.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		tokenService.Verify(x => x.IssueRefreshToken(
			It.IsAny<Func<string, RefreshToken>>()
		), Times.Once());
		tokenService.Verify(x => x.IssueTokens(User, RefreshToken), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		LoginUserCommand command = new(
			Username: User.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Act
		TokensDto tokens = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(Tokens, tokens);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenPasswordIncorrect()
	{
		// Arrange
		service.Setup(x => x.CheckPasswordAsync(User.Username, MinValidPassword)).ReturnsAsync(false);

		LoginUserCommand command = new(
			Username: User.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserLockedOut()
	{
		// Arrange
		service.Setup(x => x.GetIsLockedOutAsync(User.Username)).ReturnsAsync(DateTimeOffset.UtcNow);

		LoginUserCommand command = new(
			Username: User.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotVerified()
	{
		// Arrange
		User unverifiedUser = CreateUser(email: new(ValidEmail, IsVerified: false));
		service.Setup(x => x.GetByUsernameAsync(unverifiedUser.Username)).ReturnsAsync(unverifiedUser);

		LoginUserCommand command = new(
			Username: unverifiedUser.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
