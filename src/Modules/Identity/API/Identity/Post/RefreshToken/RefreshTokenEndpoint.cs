using CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Shared.API.Attributes;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.API.Identity.Post.RefreshToken;

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
				Token: HttpContext.GetRefreshTokenCookie()
			),
			ct: ct
		).ConfigureAwait(false);

		HttpContext.SaveAccessTokenCookie(tokens.AccessToken, settings.Value.Domain);
		HttpContext.SaveCsrfTokenCookie(tokens.CsrfToken, settings.Value.Domain);
		await Send.OkAsync().ConfigureAwait(false);
	}
}
