using CustomCADs.Modules.Identity.Domain.Users;
using CustomCADs.Modules.Identity.Infrastructure.Identity.Context;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Shared.Persistence;
using CustomCADs.Shared.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;

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

		public async Task ExecuteDbMigrationUpdaterAsync()
		{
			using IServiceScope scope = services.BuildServiceProvider().CreateScope();
			IServiceProvider provider = scope.ServiceProvider;
			await provider.UpdateIdentityContextAsync().ConfigureAwait(false);
		}
	}

}
