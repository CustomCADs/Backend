using CustomCADs.Modules.Identity.API;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.SSO.Register;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	extension(AuthenticationBuilder authentication)
	{
		public AuthenticationBuilder AddSSO(IConfiguration config, params string[] providers)
		{
			foreach (string provider in providers)
			{
				string path = $"SSO:{provider}";
				authentication.AddOpenIdConnect(provider, opts => opts.Configure(
					sso: config.GetSection(path).Get<SSOClientSettings>() ?? throw new($"Missing {path} env vars"),
					domain: config.GetSection("ServerURLs").GetValue<string>("Preferred") ?? throw new($"Missing Preferred Server URL"),
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

			return authentication;
		}
	}

	extension(OpenIdConnectOptions opts)
	{
		internal void Configure(SSOClientSettings sso, string domain, string provider, string authority, List<string> scopes)
		{
			opts.Authority = authority;
			scopes.ForEach(opts.Scope.Add);

			opts.ClientId = sso.ClientId;
			opts.ClientSecret = sso.ClientSecret;

			opts.ResponseType = OpenIdConnectResponseType.Code;
			opts.CallbackPath = $"/api/v1/{APIConstants.Paths.Identity}/sso/callback/{provider}";
			opts.GetClaimsFromUserInfoEndpoint = true;

			opts.Events = new()
			{
				OnRedirectToIdentityProvider = (ctx) =>
				{
					ctx.ProtocolMessage.RedirectUri = domain + ctx.Options.CallbackPath;
					return Task.CompletedTask;
				},
				OnTokenValidated = async (ctx) =>
				{
					CancellationToken ct = ctx.Request.HttpContext.RequestAborted;
					IServiceProvider sp = ctx.Request.HttpContext.RequestServices;

					if (ctx.Principal is null) throw new Exception("Claims required");
					ctx.Principal.ExtractUserFromSSO(out string email, out string username);

					string? role = null;
					ctx.Properties?.Items.TryGetValue("role", out role);

					IRequestSender sender = sp.GetRequiredService<IRequestSender>();
					CookieSettings cookie = sp.GetRequiredService<IOptions<CookieSettings>>().Value;

					ctx.HttpContext.SaveAllCookies(
						domain: cookie.Domain,
						username: username,
						tokens: await sender.SendCommandAsync(
							command: new SingleSignOnUserCommand(
								Role: role,
								Username: username,
								Email: email,
								Provider: provider
							),
							ct: ct
						).ConfigureAwait(false)
					);
				},
			};
		}
	}
}
