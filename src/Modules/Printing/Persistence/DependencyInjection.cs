using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Modules.Printing.Persistence;
using CustomCADs.Modules.Printing.Persistence.Repositories;
using CustomCADs.Shared.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using CustomizationReads = CustomCADs.Modules.Printing.Persistence.Repositories.Customizations.Reads;
using MaterialReads = CustomCADs.Modules.Printing.Persistence.Repositories.Materials.Reads;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdatePrintingContextAsync()
		{
			PrintingContext context = provider.GetRequiredService<PrintingContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddPrintingPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<PrintingContext>(options =>
				options.UseNpgsql(connectionString, opt
					=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Printing)
				)
			);

		private IServiceCollection AddReads()
			=> services
				.AddScoped<ICustomizationReads, CustomizationReads>()
				.AddScoped<IMaterialReads, MaterialReads>();

		private IServiceCollection AddWrites()
			=> services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		private IServiceCollection AddUnitOfWork()
			=> services.AddScoped<IUnitOfWork, UnitOfWork>();
	}

}
