using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Events;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;

using static ApplicationConstants;

public sealed class VerificationEmailHandler(
	IUserService service,
	IEventRaiser raiser,
	IOptions<ClientUrlSettings> settings
) : ICommandHandler<VerificationEmailCommand>
{
	private readonly ClientUrlSettings clientUrls = settings.Value;

	public async Task Handle(VerificationEmailCommand req, CancellationToken ct)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);
		await raiser.RaiseApplicationEventAsync(
			@event: new EmailVerificationRequestedApplicationEvent(
				Email: user.Email.Value,
				Endpoint: string.Format(
					format: Pages.ConfirmEmailPage,
					arg0: clientUrls.Preferred,
					arg1: req.Username,
					arg2: await service.GenerateEmailConfirmationTokenAsync(req.Username).ConfigureAwait(false)
				)
			)
		).ConfigureAwait(false);
	}
}
