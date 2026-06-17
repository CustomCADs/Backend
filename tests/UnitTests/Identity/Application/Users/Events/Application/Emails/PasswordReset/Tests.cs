using CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly PasswordResetRequestedHandler handler;
	private readonly PasswordResetRequestedApplicationEvent request = new(Email, Endpoint);

	private readonly Mock<IEmailService> email = new();

	private const string Email = "recipient@gmail.com";
	private const string Endpoint = "www.site.com";

	public Tests()
	{
		handler = new(email.Object);
	}

	[Fact]
	public async Task Handle_ShouldSendEmails()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		email.Verify(
			x => x.SendForgotPasswordEmailAsync(Email, Endpoint, ct),
			Times.Once()
		);
	}
}
