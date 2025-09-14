using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Accounts.Persistence;
using CustomCADs.Accounts.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using AccountReads = CustomCADs.Accounts.Persistence.Repositories.Accounts.Reads;
using AccountWrites = CustomCADs.Accounts.Persistence.Repositories.Accounts.Writes;
using RoleReads = CustomCADs.Accounts.Persistence.Repositories.Roles.Reads;
using RoleWrites = CustomCADs.Accounts.Persistence.Repositories.Roles.Writes;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateAccountsContextAsync(this IServiceProvider provider)
	{
		AccountsContext context = provider.GetRequiredService<AccountsContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddAccountsPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<AccountsContext>(options =>
			options.UseNpgsql(connectionString, opt =>
				opt.MigrationsHistoryTable(MigrationsTable, Schemes.Accounts)
			)
		);

	private static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<IRoleReads, RoleReads>();
		services.AddScoped<IAccountReads, AccountReads>();

		return services;
	}

	private static IServiceCollection AddWrites(this IServiceCollection services)
	{
		services.AddScoped<IAccountWrites, AccountWrites>();
		services.AddScoped<IRoleWrites, RoleWrites>();

		return services;
	}

	private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
