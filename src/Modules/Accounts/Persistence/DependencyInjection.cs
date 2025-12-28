using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Modules.Accounts.Persistence;
using CustomCADs.Modules.Accounts.Persistence.Repositories;
using CustomCADs.Shared.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using AccountReads = CustomCADs.Modules.Accounts.Persistence.Repositories.Accounts.Reads;
using AccountWrites = CustomCADs.Modules.Accounts.Persistence.Repositories.Accounts.Writes;
using RoleReads = CustomCADs.Modules.Accounts.Persistence.Repositories.Roles.Reads;
using RoleWrites = CustomCADs.Modules.Accounts.Persistence.Repositories.Roles.Writes;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateAccountsContextAsync()
		{
			AccountsContext context = provider.GetRequiredService<AccountsContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddAccountsPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<AccountsContext>(options =>
				options.UseNpgsql(connectionString, opt =>
					opt.MigrationsHistoryTable(MigrationsTable, Schemes.Accounts)
				)
			);

		private IServiceCollection AddReads()
			=> services
				.AddScoped<IRoleReads, RoleReads>()
				.AddScoped<IAccountReads, AccountReads>();

		private IServiceCollection AddWrites()
			=> services
				.AddScoped<IRoleWrites, RoleWrites>()
				.AddScoped<IAccountWrites, AccountWrites>();

		private IServiceCollection AddUnitOfWork()
			=> services
				.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
