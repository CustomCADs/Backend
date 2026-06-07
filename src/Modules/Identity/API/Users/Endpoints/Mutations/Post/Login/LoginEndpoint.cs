using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Login;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Shared.API.Attributes;
using Microsoft.Extensions.Options;

namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Post.Login;

public sealed class LoginEndpoint(IRequestSender sender, IFingerprintService fingerprintService, IOptions<CookieSettings> settings)
	: Endpoint<LoginRequest>
{
	public override void Configure()
	{
		Post("login");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Login")
			.WithDescription("Log in to your account")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
	{
		TokensDto tokens = await sender.SendCommandAsync(
			command: new LoginUserCommand(
				Username: req.Username,
				Password: req.Password,
				LongerExpireTime: req.RememberMe ?? false,
				Fingerprint: fingerprintService.GetFingerprint(HttpContext.Request.HeadersDictionary)
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
