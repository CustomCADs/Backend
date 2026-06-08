using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Events;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public class PasswordResetRequestedEventHandler(IEmailService email)
	: IEventHandler<PasswordResetRequestedApplicationEvent>
{
	public async Task HandleAsync(PasswordResetRequestedApplicationEvent @event)
	{
		await email.SendForgotPasswordEmailAsync(@event.Email, @event.Endpoint).ConfigureAwait(false);
	}
}
