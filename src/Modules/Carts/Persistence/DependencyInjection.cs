using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Carts.Persistence;
using CustomCADs.Carts.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using ActiveCartReads = CustomCADs.Carts.Persistence.Repositories.ActiveCarts.Reads;
using PurchasedCartReads = CustomCADs.Carts.Persistence.Repositories.PurchasedCarts.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateCartsContextAsync(this IServiceProvider provider)
	{
		CartsContext context = provider.GetRequiredService<CartsContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddCartsPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<CartsContext>(options =>
			options.UseNpgsql(connectionString, opt
				=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Carts)
			)
		);

	public static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<IActiveCartReads, ActiveCartReads>();
		services.AddScoped<IPurchasedCartReads, PurchasedCartReads>();

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
