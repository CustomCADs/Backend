using CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly PasswordResetRequestedEventHandler handler;
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
		PasswordResetRequestedApplicationEvent @event = new(Email, Endpoint);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		email.Verify(
			x => x.SendForgotPasswordEmailAsync(Email, Endpoint, ct),
			Times.Once()
		);
	}
}
