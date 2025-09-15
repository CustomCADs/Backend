using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Events;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

using static ApplicationConstants;

public sealed class ResetPasswordEmailHandler(
	IUserService service,
	IEventRaiser raiser,
	IOptions<ClientUrlSettings> settings
) : ICommandHandler<ResetPasswordEmailCommand>
{
	private readonly ClientUrlSettings clientUrls = settings.Value;

	public async Task Handle(ResetPasswordEmailCommand req, CancellationToken ct)
		=> await raiser.RaiseApplicationEventAsync(
			@event: new PasswordResetRequestedApplicationEvent(
				Email: req.Email,
				Endpoint: string.Format(
					format: Pages.ResetPasswordPage,
					arg0: clientUrls.Preferred,
					arg1: req.Email,
					arg2: await service.GeneratePasswordResetTokenAsync(req.Email).ConfigureAwait(false)
				)
			)
		).ConfigureAwait(false);
}
