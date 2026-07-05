using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.SSO.Register;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.SSO;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly SingleSignOnUserHandler handler;
	private readonly SingleSignOnUserCommand request = new(
		Role: ValidRole,
		FirstName: null,
		LastName: null,
		Username: MaxValidUsername,
		Email: ValidEmail,
		Provider: Provider,
		Fingerprint: ValidFingerprint
	);

	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Provider = "Google";
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
		handler = new(service.Object, tokenService.Object, sender.Object);

		tokenService.Setup(x => x.IssueRefreshToken(
			It.IsAny<Func<string, RefreshToken>>())
		).Returns(RefreshToken);
		tokenService.Setup(x => x.IssueTokens(user, RefreshToken)).Returns(Tokens);

		service.Setup(x => x.GetExistsByUsernameAsync(user.Username)).ReturnsAsync(true);
		service.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateAccountCommand>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(ValidAccountId);
	}

	[Test]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetExistsByUsernameAsync(user.Username),
			Times.Once()
		);
		service.Verify(
			x => x.GetExistsByEmailAsync(user.Email.Value),
			Times.Once()
		);

		service.Verify(
			x => x.SaveRefreshTokensAsync(user),
			Times.Once()
		);
		service.Verify(
			x => x.GetByUsernameAsync(user.Username),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldCallGetByEmail_WhenUsernameDoesNotExist()
	{
		// Arrange
		service.Setup(x => x.GetExistsByUsernameAsync(user.Username)).ReturnsAsync(false);
		service.Setup(x => x.GetExistsByEmailAsync(user.Email.Value)).ReturnsAsync(true);
		service.Setup(x => x.GetByEmailAsync(user.Email.Value)).ReturnsAsync(user);

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.GetByEmailAsync(user.Email.Value),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldCallCreateUser_WhenUsernameAndEmailDoNotExist()
	{
		// Arrange
		service.Setup(x => x.GetExistsByUsernameAsync(user.Username)).ReturnsAsync(false);
		service.Setup(x => x.GetExistsByEmailAsync(user.Email.Value)).ReturnsAsync(false);

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<CreateAccountCommand>(x =>
					x.Role == request.Role
					&& x.Username == request.Username
					&& x.Email == request.Email
				),
				ct
			),
			Times.Once()
		);
		service.Verify(
			x => x.CreateSSOAsync(
				It.Is<User>(x =>
					x.Username == request.Username
					&& x.AccountId == ValidAccountId
				),
				Provider
			),
			Times.Once()
		);
		service.Verify(
			x => x.GetByUsernameAsync(user.Username),
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
			x => x.IssueTokens(user, RefreshToken),
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
}