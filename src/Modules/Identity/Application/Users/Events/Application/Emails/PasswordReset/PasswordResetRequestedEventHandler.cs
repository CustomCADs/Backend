using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class PasswordResetRequestedEventHandler(IEmailService email)
{
	public async Task HandleAsync(PasswordResetRequestedApplicationEvent @event)
	{
		await email.SendForgotPasswordEmailAsync(@event.Email, @event.Endpoint).ConfigureAwait(false);
	}
}
