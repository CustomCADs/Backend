using CustomCADs.Delivery.Infrastructure;
using CustomCADs.Files.Infrastructure;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Infrastructure.Identity.Context;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Identity.Infrastructure.Tokens;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Persistence;
using CustomCADs.Shared.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using static UserConstants;

public static class ProgramExtensions
{
	private static string GetConnectionString(this IConfiguration config)
		=> config.GetConnectionString(ConnectionString) ?? throw DatabaseConnectionException.Missing(ConnectionString);

	public static IServiceCollection AddUseCases(this IServiceCollection services, IWebHostEnvironment env, bool? overrideCodeGenTo = null)
	{
		services.AddMessagingServices(
			codeGen: !env.IsDevelopment(),
			entry: CustomCADs.Tools.CodeGen.CodeGenReference.Assembly,
			assemblies: [
				CustomCADs.Accounts.Application.AccountApplicationReference.Assembly,
				CustomCADs.Carts.Application.CartsApplicationReference.Assembly,
				CustomCADs.Carts.Infrastructure.CartsInfrastructureReference.Assembly,
				CustomCADs.Catalog.Application.CatalogApplicationReference.Assembly,
				CustomCADs.Customs.Application.CustomsApplicationReference.Assembly,
				CustomCADs.Customs.Infrastructure.CustomsInfrastructureReference.Assembly,
				CustomCADs.Delivery.Application.DeliveryApplicationReference.Assembly,
				CustomCADs.Files.Application.FilesApplicationReference.Assembly,
				CustomCADs.Idempotency.Application.IdempotencyApplicationReference.Assembly,
				CustomCADs.Notifications.Application.NotificationsApplicationReference.Assembly,
				CustomCADs.Printing.Application.PrintingApplicationReference.Assembly,
				CustomCADs.Identity.Application.IdentityApplicationReference.Assembly,
			]
		);
		services.AddSagas();

		return services;
	}

	public static IServiceCollection AddCache(this IServiceCollection services)
	{
		services.AddCacheService();

		services
			.AddRoleCaching()
			.AddCategoryCaching()
			.AddTagCaching()
			.AddImageCaching()
			.AddCadCaching()
			.AddMaterialCaching();

		return services;
	}

	public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<EmailSettings>(config.GetSection("Email"));
		services.AddEmailService();

		return services;
	}

	public static IServiceCollection AddTokensService(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<JwtSettings>(config.GetSection("Jwt"));
		services.AddTokensService();

		return services;
	}

	public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration config)
	{
		IConfigurationSection section = config.GetSection("Payment");
		services.Configure<PaymentSettings>(section);

		Stripe.StripeConfiguration.ApiKey = section.Get<PaymentSettings>()?.SecretKey;
		services.AddPaymentService();

		return services;
	}

	public static IServiceCollection AddDeliveryService(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<DeliverySettings>(config.GetSection("Delivery"));
		services.AddDeliveryService();

		return services;
	}

	public static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<StorageSettings>(config.GetSection("Storage"));
		services.AddStorageService();

		services.AddCadStorageService();
		services.AddImageStorageService();

		return services;
	}

	public static IServiceCollection AddRealTimeNotifiers(this IServiceCollection services)
	{
		services.AddSignalR();

		services
			.AddNotificationsRealTimeNotifier();

		return services;
	}

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

	public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartzHostedService(opts => opts = new()
		{
			WaitForJobsToComplete = true,
			AwaitApplicationStarted = true,
			StartDelay = TimeSpan.FromMinutes(30),
		});
		services.AddQuartz(configurator =>
		{
			configurator.AddSharedBackgroundJobs();
			configurator.AddCatalogBackgroundJobs();
			configurator.AddDeliveryBackgroundJobs();
			configurator.AddIdempotencyBackgroundJobs();
			configurator.AddIdentityBackgroundJobs();
		});

		return services;
	}

	public static IServiceCollection AddAccessPolicies(this IServiceCollection services)
	{
		services.AddPurchasedCartsAccessPolicies();
		services.AddProductsAccessPolicies();
		services.AddCustomsAccessPolicies();
		services.AddMaterialsAccessPolicies();

		return services;
	}


	private static IServiceCollection AddSagas(this IServiceCollection services)
	{
		services.AddCartDeliveryPaymentSagaDependencies();
		services.AddCustomDeliveryPaymentSagaDependencies();

		return services;
	}
}
