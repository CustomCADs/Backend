using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Modules.Delivery.Persistence;
using CustomCADs.Modules.Delivery.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using ShipmentReads = CustomCADs.Modules.Delivery.Persistence.Repositories.Shipments.Reads;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateDeliveryContextAsync()
		{
			DeliveryContext context = provider.GetRequiredService<DeliveryContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddDeliveryPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<DeliveryContext>(options =>
				options.UseNpgsql(connectionString, opt
					=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Delivery)
				)
			);

		public IServiceCollection AddReads()
			=> services.AddScoped<IShipmentReads, ShipmentReads>();

		public IServiceCollection AddWrites()
			=> services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		public IServiceCollection AddUnitOfWork()
			=> services.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
