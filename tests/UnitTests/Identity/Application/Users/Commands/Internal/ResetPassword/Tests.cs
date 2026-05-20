using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPassword;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ResetPassword;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly ResetUserPasswordHandler handler;
	private readonly Mock<IUserService> service = new();

	private const string Token = "email-token";
	private readonly User user = CreateUser();

	public Tests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		ResetUserPasswordCommand command = new(
			Email: user.Email.Value,
			Token: Token,
			NewPassword: MinValidPassword
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.ResetPasswordAsync(user.Email.Value, Token, MinValidPassword), Times.Once());
	}
}
