using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.VerifyEmail;

using static UsersData;

public class VerifyUserEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly VerifyUserEmailHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();

	private const string Token = "email-token";
	private readonly User User = CreateUser(email: new(ValidEmail, IsVerified: false));
	private static readonly RefreshToken RefreshToken = RefreshToken.Create(Token, ValidId, false);
	private static readonly TokensDto Tokens = new(
		Role: "role",
		AccessToken: new("access-token", DateTimeOffset.UtcNow),
		RefreshToken: new("refresh-token", DateTimeOffset.UtcNow),
		CsrfToken: new("csrf-token", DateTimeOffset.UtcNow)
	);

	public VerifyUserEmailHandlerUnitTests()
	{
		handler = new(service.Object, tokenService.Object);

		tokenService.Setup(x => x.IssueRefreshToken(
			It.IsAny<Func<string, RefreshToken>>()
		)).Returns(RefreshToken);
		tokenService.Setup(x => x.IssueTokens(User, RefreshToken)).Returns(Tokens);

		service.Setup(x => x.GetByUsernameAsync(User.Username)).ReturnsAsync(User);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		VerifyUserEmailCommand command = new(User.Username, Token);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByUsernameAsync(User.Username), Times.Once());
		service.Verify(x => x.ConfirmEmailAsync(User.Username, Token), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldIssueTokens()
	{
		// Arrange
		VerifyUserEmailCommand command = new(User.Username, Token);

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
		VerifyUserEmailCommand command = new(User.Username, Token);

		// Act
		TokensDto tokens = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(Tokens, tokens);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenEmailVerified()
	{
		// Arrange
		User verifiedUser = CreateUser(email: new(ValidEmail, IsVerified: true));
		service.Setup(x => x.GetByUsernameAsync(verifiedUser.Username)).ReturnsAsync(verifiedUser);

		VerifyUserEmailCommand command = new(verifiedUser.Username, Token);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
