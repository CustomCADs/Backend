using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Modules.Carts.Persistence;
using CustomCADs.Modules.Carts.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using ActiveCartReads = CustomCADs.Modules.Carts.Persistence.Repositories.ActiveCarts.Reads;
using PurchasedCartReads = CustomCADs.Modules.Carts.Persistence.Repositories.PurchasedCarts.Reads;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateCartsContextAsync()
		{
			CartsContext context = provider.GetRequiredService<CartsContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddCartsPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<CartsContext>(options =>
				options.UseNpgsql(connectionString, opt =>
					opt.MigrationsHistoryTable(MigrationsTable, Schemes.Carts)
				)
			);

		private IServiceCollection AddReads()
			=> services
				.AddScoped<IActiveCartReads, ActiveCartReads>()
				.AddScoped<IPurchasedCartReads, PurchasedCartReads>();

		private IServiceCollection AddWrites()
			=> services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		private IServiceCollection AddUnitOfWork()
			=> services
				.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
