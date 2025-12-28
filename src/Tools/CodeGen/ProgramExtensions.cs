using CustomCADs.Modules.Delivery.Infrastructure;
using CustomCADs.Modules.Files.Infrastructure;
using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Infrastructure.Identity.Context;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Modules.Identity.Infrastructure.Tokens;
using CustomCADs.Modules.Printing.Domain.Services;
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
	extension(IConfiguration config)
	{
		internal string ConnectionString => config.GetConnectionString(ConnectionStringKey) ?? throw DatabaseConnectionException.Missing(ConnectionStringKey);
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddUseCases(IWebHostEnvironment env, bool? overrideCodeGenTo = null)
		{
			services.AddMessagingServices(
				codeGen: overrideCodeGenTo ?? !env.IsDevelopment(),
				entry: CustomCADs.Tools.CodeGen.CodeGenReference.Assembly,
				assemblies: [
					CustomCADs.Modules.Accounts.Application.AccountApplicationReference.Assembly,
					CustomCADs.Modules.Carts.Application.CartsApplicationReference.Assembly,
					CustomCADs.Modules.Carts.Infrastructure.CartsInfrastructureReference.Assembly,
					CustomCADs.Modules.Catalog.Application.CatalogApplicationReference.Assembly,
					CustomCADs.Modules.Customs.Application.CustomsApplicationReference.Assembly,
					CustomCADs.Modules.Customs.Infrastructure.CustomsInfrastructureReference.Assembly,
					CustomCADs.Modules.Delivery.Application.DeliveryApplicationReference.Assembly,
					CustomCADs.Modules.Files.Application.FilesApplicationReference.Assembly,
					CustomCADs.Modules.Idempotency.Application.IdempotencyApplicationReference.Assembly,
					CustomCADs.Modules.Notifications.Application.NotificationsApplicationReference.Assembly,
					CustomCADs.Modules.Printing.Application.PrintingApplicationReference.Assembly,
					CustomCADs.Modules.Identity.Application.IdentityApplicationReference.Assembly,
				]
			);
			services.AddSagas();

			return services;
		}

		public IServiceCollection AddCache()
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

		public IServiceCollection AddEmailService(IConfiguration config)
		{
			services.Configure<EmailSettings>(config.GetSection("Email"));
			services.AddEmailService();

			return services;
		}

		public IServiceCollection AddTokensService(IConfiguration config)
		{
			services.Configure<JwtSettings>(config.GetSection("Jwt"));
			services.AddTokensService();

			return services;
		}

		public IServiceCollection AddPaymentService(IConfiguration config)
		{
			IConfigurationSection section = config.GetSection("Payment");
			services.Configure<PaymentSettings>(section);

			Stripe.StripeConfiguration.ApiKey = section.Get<PaymentSettings>()?.SecretKey;
			services.AddPaymentService();

			return services;
		}

		public IServiceCollection AddDeliveryService(IConfiguration config)
		{
			services.Configure<DeliverySettings>(config.GetSection("Delivery"));
			services.AddDeliveryService();

			return services;
		}

		public IServiceCollection AddStorageService(IConfiguration config)
		{
			services.Configure<StorageSettings>(config.GetSection("Storage"));
			services.AddStorageService();

			services.AddCadStorageService();
			services.AddImageStorageService();

			return services;
		}

		public IServiceCollection AddRealTimeNotifiers()
		{
			services.AddSignalR();

			services
				.AddNotificationsRealTimeNotifier();

			return services;
		}

		public IServiceCollection AddIdentity(IConfiguration config)
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

			services.AddIdentityServices(config.ConnectionString);

			return services;
		}

		public IServiceCollection AddPersistence(IConfiguration config)
			=> services
				.AddAccountsPersistence(config.ConnectionString)
				.AddCartsPersistence(config.ConnectionString)
				.AddCatalogPersistence(config.ConnectionString)
				.AddCustomsPersistence(config.ConnectionString)
				.AddDeliveryPersistence(config.ConnectionString)
				.AddFilesPersistence(config.ConnectionString)
				.AddIdempotencyPersistence(config.ConnectionString)
				.AddNotificationsPersistence(config.ConnectionString)
				.AddPrintingPersistence(config.ConnectionString);


		public IServiceCollection AddDomainServices()
		{
			services.AddScoped<IPrintCalculator, PrintCalculator>();

			return services;
		}

		public IServiceCollection AddBackgroundJobs()
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

		public IServiceCollection AddAccessPolicies()
		{
			services.AddPurchasedCartsAccessPolicies();
			services.AddProductsAccessPolicies();
			services.AddCustomsAccessPolicies();
			services.AddMaterialsAccessPolicies();

			return services;
		}


		private IServiceCollection AddSagas()
		{
			services.AddCartDeliveryPaymentSagaDependencies();
			services.AddCustomDeliveryPaymentSagaDependencies();

			return services;
		}
	}

}
