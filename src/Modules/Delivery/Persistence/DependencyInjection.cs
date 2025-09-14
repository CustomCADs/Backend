using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Delivery.Persistence;
using CustomCADs.Delivery.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using ShipmentReads = CustomCADs.Delivery.Persistence.Repositories.Shipments.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateDeliveryContextAsync(this IServiceProvider provider)
	{
		DeliveryContext context = provider.GetRequiredService<DeliveryContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddDeliveryPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<DeliveryContext>(options =>
			options.UseNpgsql(connectionString, opt
				=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Delivery)
			)
		);

	public static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<IShipmentReads, ShipmentReads>();

		return services;
	}

	public static IServiceCollection AddWrites(this IServiceCollection services)
	{
		services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		return services;
	}

	public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
