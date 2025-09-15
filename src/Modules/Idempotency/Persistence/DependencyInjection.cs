using CustomCADs.Idempotency.Domain.Repositories;
using CustomCADs.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Idempotency.Persistence;
using CustomCADs.Idempotency.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using IdempotencyKeyReads = CustomCADs.Idempotency.Persistence.Repositories.IdempotencyKeys.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateIdempotencyContextAsync(this IServiceProvider provider)
	{
		IdempotencyContext context = provider.GetRequiredService<IdempotencyContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddIdempotencyPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<IdempotencyContext>(options =>
			options.UseNpgsql(connectionString, opt
				=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Idempotency)
			)
		);

	private static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<IIdempotencyKeyReads, IdempotencyKeyReads>();

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
