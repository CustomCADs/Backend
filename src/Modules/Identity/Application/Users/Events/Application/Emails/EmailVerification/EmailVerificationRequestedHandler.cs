using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Events;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.EmailVerification;

public class EmailVerificationRequestedHandler(IEmailService email)
	: IEventHandler<EmailVerificationRequestedApplicationEvent>
{
	public async Task HandleAsync(EmailVerificationRequestedApplicationEvent @event)
	{
		await email.SendVerificationEmailAsync(@event.Email, @event.Endpoint).ConfigureAwait(false);
	}
}
