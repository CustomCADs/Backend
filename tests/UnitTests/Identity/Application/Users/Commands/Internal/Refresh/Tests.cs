using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Refresh;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;
using static CustomCADs.Modules.Identity.Domain.Constants.Tokens;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Refresh;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly RefreshUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();

	private static readonly RefreshToken RefreshToken = RefreshToken.Create("refresh-token", ValidFingerprint, ValidId, longerSession: false);
	private readonly User User = CreateUser(username: MaxValidUsername);
	private static readonly TokensDto Tokens = new(
		Role: "role",
		AccessToken: new("access-token", DateTimeOffset.UtcNow),
		RefreshToken: new("refresh-token", DateTimeOffset.UtcNow),
		CsrfToken: new("csrf-token", DateTimeOffset.UtcNow)
	);

	public Tests()
	{
		handler = new(service.Object, tokenService.Object);

		tokenService.Setup(x => x.IssueTokens(User, RefreshToken)).Returns(Tokens);

		service.Setup(x => x.GetByRefreshTokenAsync(RefreshToken.Value)).ReturnsAsync((User, RefreshToken));
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		RefreshUserCommand command = new(RefreshToken.Value, ValidFingerprint);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(
			x => x.GetByRefreshTokenAsync(RefreshToken.Value),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldIssueTokens()
	{
		// Arrange
		RefreshUserCommand command = new(RefreshToken.Value, ValidFingerprint);

		// Act
		await handler.Handle(command, ct);

		// Assert
		tokenService.Verify(
			x => x.IssueTokens(User, RefreshToken),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		RefreshUserCommand command = new(RefreshToken.Value, ValidFingerprint);

		// Act
		TokensDto tokens = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(Tokens, tokens);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenMissingToken()
	{
		// Arrange
		RefreshUserCommand command = new(Token: null, ValidFingerprint);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenTokenExpired()
	{
		// Arrange
		DateTimeOffset yesterday = DateTimeOffset.UtcNow.AddDays(-1);
		RefreshToken token = RefreshToken.Create(
			id: RefreshTokenId.New(),
			value: "refresh-token",
			fingerprint: ValidFingerprint,
			userId: ValidId,
			issuedAt: yesterday.AddDays(-RtDurationInDays),
			expiresAt: yesterday
		);
		service.Setup(x => x.GetByRefreshTokenAsync(token.Value)).ReturnsAsync((User, token));

		RefreshUserCommand command = new(token.Value, ValidFingerprint);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
