using CustomCADs.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;

	extension(IServiceCollection services)
	{
		public AuthenticationBuilder AddAuthN(string scheme = AuthScheme)
			=> services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = scheme;
				opt.DefaultForbidScheme = scheme;
				opt.DefaultSignInScheme = scheme;
				opt.DefaultSignOutScheme = scheme;
				opt.DefaultChallengeScheme = scheme;
				opt.DefaultScheme = scheme;
			});
	}

	extension(AuthenticationBuilder authentication)
	{
		public AuthenticationBuilder AddJwt((string SecretKey, string Issuer, string Audience) settings)
			=> authentication.AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new()
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = settings.Issuer,
					ValidAudience = settings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(
						key: Encoding.UTF8.GetBytes(settings.SecretKey)
					),
				};

				opt.Events = new()
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["jwt"];
						return Task.CompletedTask;
					},

					OnChallenge = async context =>
					{
						context.HandleResponse();

						await context.HttpContext.RequestServices
						   .GetRequiredService<IProblemDetailsService>()
						   .UnauthorizedResponseAsync(
								context: context.HttpContext,
								ex: new UnauthorizedAccessException()
						   ).ConfigureAwait(false);
					},

					OnForbidden = async context =>
					{
						await context.HttpContext.RequestServices
						   .GetRequiredService<IProblemDetailsService>()
						   .ForbiddenResponseAsync(
								context: context.HttpContext,
								ex: new AccessViolationException()
						   ).ConfigureAwait(false);
					},

					OnTokenValidated = context =>
					{
						ClaimsIdentity claimsIdentity = new(context.Principal?.Claims ?? [], AuthScheme);
						context.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
						return Task.CompletedTask;
					},
				};
			});
	}

	extension(IApplicationBuilder app)
	{
		public IApplicationBuilder UseJwtPrincipal()
			=> app.Use(async (context, next) =>
			{
				string? accessToken = context.Request.Cookies["jwt"];
				if (!string.IsNullOrWhiteSpace(accessToken))
				{
					if (new JwtSecurityTokenHandler().ReadToken(accessToken) is JwtSecurityToken jwt)
					{
						ClaimsIdentity identity = new(jwt.Claims, AuthScheme);
						context.User = new(identity);
					}
				}

				await next().ConfigureAwait(false);
			});

		public IApplicationBuilder UseCsrfProtection()
			=> app.Use(async (context, next) =>
			{
				if (
					!context.Request.IsSignalR // isn't a websocket request
					&& context.Request.IsMutationBySpec // might mutate state
					&& context.User.IsAuthenticated // has access to sensitive info
					&& context.Request.IsCsrfVulnerable // no csrf protection
				)
				{
					await context.RequestServices
					   .GetRequiredService<IProblemDetailsService>()
					   .ForbiddenResponseAsync(
							context: context,
							ex: new CustomException("CSRF token validation failed: cookie and header mismatch."),
							message: "CSRF token mismatch."
						).ConfigureAwait(false);
					return;
				}
				await next().ConfigureAwait(false);
			});
	}

	extension(HttpRequest request)
	{
		internal bool IsCsrfVulnerable
		{
			get
			{
				string? cookie = request.Cookies["csrf"];
				string? header = request.Headers["Csrf-Token"];
				return string.IsNullOrEmpty(cookie) || string.IsNullOrEmpty(header) || !cookie.Equals(header);
			}
		}
	}
}
