using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.VerifyEmail;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly VerifyUserEmailHandler handler;
	private readonly VerifyUserEmailCommand request = new(MinValidUsername, RefreshTokenValue, ValidFingerprint);

	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();

	private const string RefreshTokenValue = "refresh-token";
	private readonly User User = CreateUser(isVerified: false);
	private static readonly RefreshToken RefreshToken = RefreshToken.Create(RefreshTokenValue, ValidFingerprint, ValidId, false);
	private static readonly TokensDto Tokens = new(
		Role: "role",
		AccessToken: new("access-token", DateTimeOffset.UtcNow),
		RefreshToken: new(RefreshTokenValue, DateTimeOffset.UtcNow),
		CsrfToken: new("csrf-token", DateTimeOffset.UtcNow)
	);

	public Tests()
	{
		handler = new(service.Object, tokenService.Object);

		tokenService.Setup(x => x.IssueRefreshToken(It.IsAny<Func<string, RefreshToken>>()))
			.Returns((Func<string, RefreshToken> factory) => factory(RefreshToken.Value));

		tokenService.Setup(x => x.IssueTokens(User, It.Is<RefreshToken>(x => x.Value == RefreshTokenValue)))
			.Returns(Tokens);

		service.Setup(x => x.GetByUsernameAsync(User.Username))
			.ReturnsAsync(User);
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetByUsernameAsync(User.Username),
			Times.Once()
		);
		service.Verify(
			x => x.ConfirmEmailAsync(User.Username, RefreshTokenValue),
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
			x => x.IssueTokens(User, It.Is<RefreshToken>(x => x.Value == RefreshToken.Value)),
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
		using (Assert.Multiple())
		{
			await Assert.That(tokens.AccessToken).IsEqualTo(Tokens.AccessToken);
			await Assert.That(tokens.RefreshToken.Value).IsEqualTo(Tokens.RefreshToken.Value);
			await Assert.That(tokens.CsrfToken).IsEqualTo(Tokens.CsrfToken);
			await Assert.That(tokens.Role).IsEqualTo(Tokens.Role);
		}
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenEmailVerified()
	{
		// Arrange
		User verifiedUser = CreateUser(email: ValidEmail, isVerified: true);
		service.Setup(x => x.GetByUsernameAsync(verifiedUser.Username)).ReturnsAsync(verifiedUser);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(() => handler.Handle(request with { Username = verifiedUser.Username }, ct));
	}
}
