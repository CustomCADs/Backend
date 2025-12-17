using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Modules.Catalog.Persistence;
using CustomCADs.Modules.Catalog.Persistence.Repositories;
using CustomCADs.Shared.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using CategoryReads = CustomCADs.Modules.Catalog.Persistence.Repositories.Categories.Reads;
using CategoryWrites = CustomCADs.Modules.Catalog.Persistence.Repositories.Categories.Writes;
using ProductReads = CustomCADs.Modules.Catalog.Persistence.Repositories.Products.Reads;
using ProductWrites = CustomCADs.Modules.Catalog.Persistence.Repositories.Products.Writes;
using TagReads = CustomCADs.Modules.Catalog.Persistence.Repositories.Tags.Reads;
using TagWrites = CustomCADs.Modules.Catalog.Persistence.Repositories.Tags.Writes;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateCatalogContextAsync()
		{
			CatalogContext context = provider.GetRequiredService<CatalogContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddCatalogPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<CatalogContext>(options =>
				options.UseNpgsql(connectionString, opt =>
					opt.MigrationsHistoryTable(MigrationsTable, Schemes.Catalog)
				)
			);

		private IServiceCollection AddReads()
		 	=> services
				.AddScoped<IProductReads, ProductReads>()
				.AddScoped<ICategoryReads, CategoryReads>()
				.AddScoped<ITagReads, TagReads>();

		private IServiceCollection AddWrites()
		 	=> services
				.AddScoped<IProductWrites, ProductWrites>()
				.AddScoped<ICategoryWrites, CategoryWrites>()
				.AddScoped<ITagWrites, TagWrites>();

		private IServiceCollection AddUnitOfWork()
			=> services.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
