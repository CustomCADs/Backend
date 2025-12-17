using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Infrastructure.Tokens;
using CustomCADs.Modules.Notifications.Infrastructure.Hubs;
using CustomCADs.Presentation;
using CustomCADs.Shared.API;
using CustomCADs.Shared.API.Extensions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
	private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;
	private const string CorsPolicy = "client";

	extension(AuthenticationBuilder authentication)
	{
		public AuthenticationBuilder AddJwt(IConfiguration config)
		{
			JwtSettings settings = config.GetSection("Jwt").Get<JwtSettings>()
				?? throw new KeyNotFoundException("JwtSettings not provided.");

			authentication.AddJwt((
				SecretKey: settings.SecretKey,
				Issuer: settings.Issuer,
				Audience: settings.Audience
			));

			return authentication;
		}
	}

	extension(IServiceCollection services)
	{
		public void AddAuthZ(params IEnumerable<string> roles)
			=> services.AddAuthorization(options =>
			{
				foreach (string role in roles)
				{
					options.AddPolicy(role, policy => policy.RequireRole(role));
				}
			});

		public IServiceCollection AddGlobalExceptionHandler()
			=> services.AddExceptionHandler<GlobalExceptionHandler>();

		public IServiceCollection AddRateLimiting()
			=> services.AddRateLimiter(options =>
				options.AddPolicy(
					policyName: APIConstants.RateLimitPolicy,
					partitioner: context =>
					{
						AccountId userId = context.User.AccountId;

						if (userId.IsEmpty())
						{
							return RateLimitPartition.GetFixedWindowLimiter(
								"anonymous",
								_ => new()
								{
									PermitLimit = RateLimitConstants.Anonymous.GlobalLimit,
									Window = RateLimitConstants.Anonymous.Window,
									QueueLimit = RateLimitConstants.Anonymous.QueueLimit,
									QueueProcessingOrder = RateLimitConstants.Anonymous.QueueOrder,
									AutoReplenishment = RateLimitConstants.Anonymous.AutoReplenish,
								}
							);
						}

						return RateLimitPartition.GetTokenBucketLimiter(
							userId.Value.ToString(),
							_ => new()
							{
								TokenLimit = RateLimitConstants.Authenticated.BurstLimit,
								ReplenishmentPeriod = RateLimitConstants.Authenticated.Period,
								TokensPerPeriod = RateLimitConstants.Authenticated.ReplenishedTokens,
								QueueLimit = RateLimitConstants.Authenticated.QueueLimit,
								QueueProcessingOrder = RateLimitConstants.Authenticated.QueueOrder,
								AutoReplenishment = RateLimitConstants.Authenticated.AutoReplenish,
							}
						);
					})
			);

		public void AddEndpoints() => services.AddFastEndpoints().AddEndpointsApiExplorer();

		public void AddJsonOptions() => services
			.ConfigureHttpJsonOptions(options =>
			{
				options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

		public void AddApiDocumentation(IConfiguration config, string version = "v1")
			=> services.AddOpenApi(version, cfg =>
			{
				cfg.AddDocumentTransformer((document, context, ct) =>
				{
					string description = """
### The best API to:
- Order and Purchase 3D Models
- Download them and have them Printed & Delivered
- Upload and Sell 3D Models
""";

					document.Info = new()
					{
						Title = "CustomCADs API",
						Description = description,
						Contact = new() { Name = "Ivan", Email = "ivanangelov414@gmail.com", },
						License = new() { Name = "Apache License 2.0", Url = new("https://www.apache.org/licenses/LICENSE-2.0"), },
						Version = version
					};
					document.Tags = document.Tags?.OrderBy(t => t.Name).ToHashSet();

					ServerUrlSettings? settings = config.GetSection("ServerURLs").Get<ServerUrlSettings>();
					if (settings is not null)
					{
						string[] serversUrls = [settings.Preferred, .. settings.All.Split(',')];
						document.Servers = [..
						serversUrls
							.Distinct()
							.Select(url => new OpenApiServer() { Url = url })
						];
					}

					return Task.CompletedTask;
				});
			});

		public void AddCorsForClient(IConfiguration config)
		{
			services.Configure<CookieSettings>(config.GetSection("Cookie"));

			IConfigurationSection section = config.GetSection("ClientURLs");
			services.Configure<ClientUrlSettings>(section);

			services.AddCors(opt =>
			{
				ClientUrlSettings urls = section.Get<ClientUrlSettings>()
					?? throw new KeyNotFoundException("URLs not provided.");

				opt.AddPolicy(CorsPolicy, builder =>
				{
					builder.WithOrigins(urls.All.Split(','))
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials();
				});
			});
		}
	}

	extension(IApplicationBuilder app)
	{
		public void UseCorsForClient() => app.UseCors(CorsPolicy);

		public IApplicationBuilder UseEndpoints()
			=> app.UseFastEndpoints(cfg =>
			{
				cfg.Endpoints.Configurator = (ep) =>
				{
					ep.AuthSchemes(AuthScheme);
					ep.Description(d => d.RequireRateLimiting(APIConstants.RateLimitPolicy));
				};
				cfg.Endpoints.RoutePrefix = "api";
				cfg.Versioning.DefaultVersion = 1;
				cfg.Versioning.PrependToRoute = true;
			});

		public IApplicationBuilder UseDisableBrowserCaching()
			=> app.Use(async (context, next) =>
			{
				if (context.Request.Path.StartsWithSegments("/api"))
				{
					IHeaderDictionary headers = context.Response.Headers;
					headers.CacheControl = "no-store, no-cache, must-revalidate, proxy-revalidate";
					headers.Pragma = "no-cache";
					headers.Expires = "0";
				}

				await next().ConfigureAwait(false);
			});
	}

	extension(IEndpointRouteBuilder router)
	{
		public IEndpointRouteBuilder MapApiDocumentationUi([StringSyntax("Route")] string apiPattern = "/openai/{documentName}.json", [StringSyntax("Route")] string uiPattern = "/scalar/{documentName}")
		{
			router.MapOpenApi(apiPattern);
			router.MapScalarApiReference(uiPattern, options =>
			{
				ScalarTheme[] themes =
				[
					ScalarTheme.BluePlanet,
					ScalarTheme.Kepler,
					ScalarTheme.Mars,
					ScalarTheme.DeepSpace,
				];

				options
					.WithOpenApiRoutePattern(apiPattern)
					.SortOperationsByMethod()
					.SortTagsAlphabetically()
					.WithTitle("CustomCADs API")
					.WithTheme(themes[Random.Shared.Next(0, themes.Length)])
					.WithFavicon("/favicon.ico")
					.HideDarkModeToggle();
			});

			return router;
		}

		public IEndpointRouteBuilder MapRealTimeHubs()
			=> router
				.MapRealTimeHub<SignalRNotificationsHub>("Notifications");

		private IEndpointRouteBuilder MapRealTimeHub<THub>(string pattern) where THub : AspNetCore.SignalR.Hub
		{
			router.MapHub<THub>($"{APIConstants.RequestPrefixForSignalR}/{pattern}");

			return router;
		}
	}
}
