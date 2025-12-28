using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Shared.API.Attributes;
using Microsoft.Extensions.Options;

namespace CustomCADs.Modules.Identity.API.Identity.Post.VerifyEmail;

public sealed class ConfirmEmailEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
	: Endpoint<ConfirmEmailRequest>
{
	public override void Configure()
	{
		Post("email/confirm");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithName(IdentityNames.ConfirmEmail)
			.WithSummary("Confirm Email")
			.WithDescription("Confirm the verification email")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ConfirmEmailRequest req, CancellationToken ct)
	{
		TokensDto tokens = await sender.SendCommandAsync(
			command: new VerifyUserEmailCommand(
				Username: req.Username,
				Token: req.Token.Replace(' ', '+')
			),
			ct: ct
		).ConfigureAwait(false);

		HttpContext.SaveAllCookies(
			domain: settings.Value.Domain,
			username: req.Username,
			tokens: tokens
		);
		await Send.OkAsync().ConfigureAwait(false);
	}
}
