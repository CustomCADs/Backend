using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.EmailVerification;

public class EmailVerificationRequestedEventHandler(IEmailService email)
{
	public async Task HandleAsync(EmailVerificationRequestedApplicationEvent @event)
	{
		await email.SendVerificationEmailAsync(@event.Email, @event.Endpoint).ConfigureAwait(false);
	}
}
