using CustomCADs.Shared.API.Attributes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.API;

using static APIConstants;

public static class SSOEndpoint
{
	public static void MapSSOLoginEndpoint(this IEndpointRouteBuilder app)
	{
		app.MapGet($"api/v1/{Paths.Identity}/sso", async (string provider, string? role, string? redirectUrl, HttpContext context, CancellationToken ct = default) =>
		{
			AuthenticationProperties props = new()
			{
				RedirectUri = redirectUrl,
			};
			props.Items["role"] = role;

			await context.ChallengeAsync(provider, props).ConfigureAwait(false);
		})
		.WithTags(Tags[Paths.Identity])
		.WithSummary("SSO")
		.WithDescription("Log in to your account via SSO")
		.WithMetadata(new SkipIdempotencyAttribute());
	}
}
