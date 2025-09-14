using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Customs.Persistence;
using CustomCADs.Customs.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using CustomReads = CustomCADs.Customs.Persistence.Repositories.Customs.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateCustomsContextAsync(this IServiceProvider provider)
	{
		CustomsContext context = provider.GetRequiredService<CustomsContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddCustomsPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<CustomsContext>(options =>
			options.UseNpgsql(connectionString, opt
				=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Customs)
			)
		);

	private static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<ICustomReads, CustomReads>();

		return services;
	}

	private static IServiceCollection AddWrites(this IServiceCollection services)
	{
		services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		return services;
	}

	private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
