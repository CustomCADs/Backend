using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Persistence;
using CustomCADs.Catalog.Persistence.Repositories;
using CustomCADs.Shared.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using CategoryReads = CustomCADs.Catalog.Persistence.Repositories.Categories.Reads;
using CategoryWrites = CustomCADs.Catalog.Persistence.Repositories.Categories.Writes;
using ProductReads = CustomCADs.Catalog.Persistence.Repositories.Products.Reads;
using ProductWrites = CustomCADs.Catalog.Persistence.Repositories.Products.Writes;
using TagReads = CustomCADs.Catalog.Persistence.Repositories.Tags.Reads;
using TagWrites = CustomCADs.Catalog.Persistence.Repositories.Tags.Writes;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateCatalogContextAsync(this IServiceProvider provider)
	{
		CatalogContext context = provider.GetRequiredService<CatalogContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<CatalogContext>(options =>
			options.UseNpgsql(connectionString, opt =>
				opt.MigrationsHistoryTable(MigrationsTable, Schemes.Catalog)
			)
		);

	private static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<IProductReads, ProductReads>();
		services.AddScoped<ICategoryReads, CategoryReads>();
		services.AddScoped<ITagReads, TagReads>();

		return services;
	}

	private static IServiceCollection AddWrites(this IServiceCollection services)
	{
		services.AddScoped<IProductWrites, ProductWrites>();
		services.AddScoped<ICategoryWrites, CategoryWrites>();
		services.AddScoped<ITagWrites, TagWrites>();

		return services;
	}

	private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
