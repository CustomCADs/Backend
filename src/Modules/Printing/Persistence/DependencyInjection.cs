using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Persistence;
using CustomCADs.Printing.Persistence.Repositories;
using CustomCADs.Shared.Persistence;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using CustomizationReads = CustomCADs.Printing.Persistence.Repositories.Customizations.Reads;
using MaterialReads = CustomCADs.Printing.Persistence.Repositories.Materials.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdatePrintingContextAsync(this IServiceProvider provider)
	{
		PrintingContext context = provider.GetRequiredService<PrintingContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddPrintingPersistence(this IServiceCollection services, string connectionString)
		=> services
			.AddContext(connectionString)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<PrintingContext>(options =>
			options.UseNpgsql(connectionString, opt
				=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Printing)
			)
		);

	private static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<ICustomizationReads, CustomizationReads>();
		services.AddScoped<IMaterialReads, MaterialReads>();

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
