using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Infrastructure.Identity.Context;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Infrastructure.Utilities;
using CustomCADs.Shared.Persistence;
using Microsoft.AspNetCore.Identity;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static UserConstants;
using static PersistenceConstants;

public static class ProgramExtensions
{
	private static string GetConnectionString(this IConfiguration config)
		=> config.GetApplicationConnectionString(ConnectionString);

	public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
	{
		services.AddIdentity<AppUser, AppRole>(options =>
		{
			options.SignIn.RequireConfirmedEmail = true;
			options.SignIn.RequireConfirmedAccount = false;
			options.Password.RequireDigit = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireUppercase = false;
			options.Password.RequiredLength = PasswordMinLength;
			options.User.RequireUniqueEmail = true;
			options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+" + ' '; // default + space
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
		})
		.AddEntityFrameworkStores<IdentityContext>()
		.AddDefaultTokenProviders();

		string? connectionString = config.GetConnectionString(ConnectionString)
			?? throw new KeyNotFoundException($"Could not find connection string '{ConnectionString}'.");

		services.AddIdentityServices(config.GetConnectionString());

		return services;
	}

	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddAccountsPersistence(config.GetConnectionString())
			.AddCartsPersistence(config.GetConnectionString())
			.AddCatalogPersistence(config.GetConnectionString())
			.AddCustomsPersistence(config.GetConnectionString())
			.AddDeliveryPersistence(config.GetConnectionString())
			.AddFilesPersistence(config.GetConnectionString())
			.AddIdempotencyPersistence(config.GetConnectionString())
			.AddNotificationsPersistence(config.GetConnectionString())
			.AddPrintingPersistence(config.GetConnectionString());

	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddScoped<IPrintCalculator, PrintCalculator>();

		return services;
	}

	public static async Task ExecuteDbMigrationUpdater(this IServiceCollection services)
	{
		using IServiceScope scope = services.BuildServiceProvider().CreateScope();
		IServiceProvider provider = scope.ServiceProvider;

		await Task.WhenAll([
			provider.UpdateAccountsContextAsync(),
			provider.UpdateCartsContextAsync(),
			provider.UpdateCatalogContextAsync(),
			provider.UpdateCustomsContextAsync(),
			provider.UpdateDeliveryContextAsync(),
			provider.UpdateFilesContextAsync(),
			provider.UpdateIdempotencyContextAsync(),
			provider.UpdateIdentityContextAsync(),
			provider.UpdateNotificationsContextAsync(),
			provider.UpdatePrintingContextAsync(),
		]).ConfigureAwait(false);
	}
}
