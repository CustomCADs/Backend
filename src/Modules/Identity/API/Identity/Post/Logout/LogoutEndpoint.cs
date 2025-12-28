using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Logout;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Shared.API.Attributes;
using Microsoft.Extensions.Options;

namespace CustomCADs.Modules.Identity.API.Identity.Post.Logout;

public sealed class LogoutEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
	: EndpointWithoutRequest<string>
{
	public override void Configure()
	{
		Post("logout");
		Group<IdentityGroup>();
		Description(x => x
			.WithName(IdentityNames.Logout)
			.WithSummary("Log out")
			.WithDescription("Log out of your account")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new LogoutUserCommand(
				RefreshToken: HttpContext.RefreshTokenCookie
			),
			ct: ct
		).ConfigureAwait(false);

		HttpContext.DeleteAllCookies(settings.Value.Domain);
	}
}
