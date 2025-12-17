using CustomCADs.Modules.Idempotency.Domain.Repositories;
using CustomCADs.Modules.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Modules.Idempotency.Persistence;
using CustomCADs.Modules.Idempotency.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using IdempotencyKeyReads = CustomCADs.Modules.Idempotency.Persistence.Repositories.IdempotencyKeys.Reads;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateIdempotencyContextAsync()
		{
			IdempotencyContext context = provider.GetRequiredService<IdempotencyContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddIdempotencyPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<IdempotencyContext>(options =>
				options.UseNpgsql(connectionString, opt
					=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Idempotency)
				)
			);

		private IServiceCollection AddReads()
			=> services.AddScoped<IIdempotencyKeyReads, IdempotencyKeyReads>();

		private IServiceCollection AddWrites()
			=> services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		private IServiceCollection AddUnitOfWork()
			=> services.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
