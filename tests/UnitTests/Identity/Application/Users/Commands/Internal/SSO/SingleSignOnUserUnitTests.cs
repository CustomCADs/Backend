using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.SSO.Register;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.SSO;

using static UsersData;

public class SingleSignOnUserUnitTests : UsersBaseUnitTests
{
	private readonly SingleSignOnUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokenService = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Provider = "Google";
	private readonly User User = CreateUser(username: MaxValidUsername);
	private static readonly RefreshToken RefreshToken = RefreshToken.Create("refresh-token", ValidId, false);
	private static readonly TokensDto Tokens = new(
		Role: "role",
		AccessToken: new("access-token", DateTimeOffset.UtcNow),
		RefreshToken: new("refresh-token", DateTimeOffset.UtcNow),
		CsrfToken: new("csrf-token", DateTimeOffset.UtcNow)
	);

	public SingleSignOnUserUnitTests()
	{
		handler = new(service.Object, tokenService.Object, sender.Object);

		tokenService.Setup(x => x.IssueRefreshToken(
			It.IsAny<Func<string, RefreshToken>>())
		).Returns(RefreshToken);
		tokenService.Setup(x => x.IssueTokens(User, RefreshToken)).Returns(Tokens);

		service.Setup(x => x.GetExistsByUsernameAsync(User.Username)).ReturnsAsync(true);
		service.Setup(x => x.GetByUsernameAsync(User.Username)).ReturnsAsync(User);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateAccountCommand>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(ValidAccountId);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		SingleSignOnUserCommand command = new(
			Role: User.Role,
			Username: User.Username,
			Email: User.Email.Value,
			Provider: Provider
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetExistsByUsernameAsync(User.Username), Times.Once());
		service.Verify(x => x.GetExistsByEmailAsync(User.Email.Value), Times.Once());

		service.Verify(x => x.SaveRefreshTokensAsync(User), Times.Once());
		service.Verify(x => x.GetByUsernameAsync(User.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallGetByEmail_WhenUsernameDoesNotExist()
	{
		// Arrange
		service.Setup(x => x.GetExistsByUsernameAsync(User.Username)).ReturnsAsync(false);
		service.Setup(x => x.GetExistsByEmailAsync(User.Email.Value)).ReturnsAsync(true);
		service.Setup(x => x.GetByEmailAsync(User.Email.Value)).ReturnsAsync(User);

		SingleSignOnUserCommand command = new(
			Role: User.Role,
			Username: User.Username,
			Email: User.Email.Value,
			Provider: Provider
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByEmailAsync(User.Email.Value), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallCreateUser_WhenUsernameAndEmailDoNotExist()
	{
		// Arrange
		service.Setup(x => x.GetExistsByUsernameAsync(User.Username)).ReturnsAsync(false);
		service.Setup(x => x.GetExistsByEmailAsync(User.Email.Value)).ReturnsAsync(false);

		SingleSignOnUserCommand command = new(
			Role: User.Role,
			Username: User.Username,
			Email: User.Email.Value,
			Provider: Provider
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<CreateAccountCommand>(x =>
				x.Role == command.Role
				&& x.Username == command.Username
				&& x.Email == command.Email
			),
			ct
		), Times.Once());
		service.Verify(x => x.CreateSSOAsync(
			It.Is<User>(x =>
				x.Username == command.Username
				&& x.AccountId == ValidAccountId
			),
			Provider
		), Times.Once());
		service.Verify(x => x.GetByUsernameAsync(User.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldIssueTokens()
	{
		// Arrange
		SingleSignOnUserCommand command = new(
			Role: User.Role,
			Username: User.Username,
			Email: User.Email.Value,
			Provider: Provider
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
		SingleSignOnUserCommand command = new(
			Role: User.Role,
			Username: User.Username,
			Email: User.Email.Value,
			Provider: Provider
		);

		// Act
		TokensDto tokens = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(Tokens, tokens);
	}
}
