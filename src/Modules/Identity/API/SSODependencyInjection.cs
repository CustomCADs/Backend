using CustomCADs.Identity.API;
using CustomCADs.Identity.Application.Users.Commands.Internal.SSO.Register;
using CustomCADs.Identity.Application.Users.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static AuthenticationBuilder AddSSO(this AuthenticationBuilder builder, IConfiguration config, params string[] providers)
	{
		foreach (string provider in providers)
		{
			string path = $"SSO:{provider}";
			builder.AddOpenIdConnect(provider, opts => opts.Configure(
				sso: config.GetSection(path).Get<SSOClientSettings>() ?? throw new($"Missing {path} env vars"),
				provider: provider,
				authority: provider switch
				{
					APIConstants.SSO.Google => "https://accounts.google.com",
					_ => throw new InvalidOperationException($"Unknown SSO provider: {provider}"),
				},
				scopes: provider switch
				{
					APIConstants.SSO.Google => ["openid", "profile", "email"],
					_ => [],
				}
			));
		}

		return builder;
	}

	private static void Configure(this OpenIdConnectOptions opts, SSOClientSettings sso, string provider, string authority, string[] scopes)
	{
		opts.Authority = authority;
		opts.ClientId = sso.ClientId;
		opts.ClientSecret = sso.ClientSecret;
		opts.ResponseType = OpenIdConnectResponseType.Code;
		opts.CallbackPath = $"/api/v1/{APIConstants.Paths.Identity}/sso/callback/{provider}";
		opts.GetClaimsFromUserInfoEndpoint = true;

		foreach (string permission in scopes)
		{
			opts.Scope.Add(permission);
		}

		opts.Events = new()
		{
			OnTokenValidated = async (ctx) =>
			{
				CancellationToken ct = ctx.Request.HttpContext.RequestAborted;
				IServiceProvider sp = ctx.Request.HttpContext.RequestServices;
				ctx.Principal.ExtractUserFromSSO(out string email, out string username);

				IRequestSender sender = sp.GetRequiredService<IRequestSender>();
				CookieSettings cookie = sp.GetRequiredService<IOptions<CookieSettings>>().Value;

				ctx.HttpContext.SaveAllCookies(
					domain: cookie.Domain,
					username: username,
					tokens: await sender.SendCommandAsync(
						command: new SingleSignOnUserCommand(
							Role: ctx.Properties?.Items["role"],
							Username: username,
							Email: email,
							Provider: provider
						),
						ct: ct
					).ConfigureAwait(false)
				);
			}
		};
	}
}
