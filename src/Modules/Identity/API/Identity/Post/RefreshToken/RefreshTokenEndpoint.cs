using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Refresh;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Shared.API.Attributes;
using Microsoft.Extensions.Options;

namespace CustomCADs.Modules.Identity.API.Identity.Post.RefreshToken;

public sealed class RefreshTokenEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Post("refresh");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithName(IdentityNames.Refresh)
			.WithSummary("Refresh")
			.WithDescription("Refresh your login")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		TokensDto tokens = await sender.SendCommandAsync(
			command: new RefreshUserCommand(
				Token: HttpContext.RefreshTokenCookie
			),
			ct: ct
		).ConfigureAwait(false);

		HttpContext.RefreshCookies(
			access: tokens.AccessToken,
			csrf: tokens.CsrfToken,
			domain: settings.Value.Domain
		);
		await Send.OkAsync().ConfigureAwait(false);
	}
}
