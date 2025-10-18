using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class PasswordResetRequestedEventHandler(IEmailService email)
{
	public async Task HandleAsync(PasswordResetRequestedApplicationEvent ae)
	{
		await email.SendForgotPasswordEmailAsync(ae.Email, ae.Endpoint).ConfigureAwait(false);
	}
}
