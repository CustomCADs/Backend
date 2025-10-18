using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Persistence;
using CustomCADs.Shared.Persistence.Exceptions;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;

public static class ProgramExtensions
{
	private static string GetConnectionString(this IConfiguration config)
		=> config.GetConnectionString(ConnectionString) ?? throw DatabaseConnectionException.Missing(ConnectionString);

	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddAccountsPersistence(config.GetConnectionString())
			.AddCartsPersistence(config.GetConnectionString())
			.AddCatalogPersistence(config.GetConnectionString())
			.AddCustomsPersistence(config.GetConnectionString())
			.AddDeliveryPersistence(config.GetConnectionString())
			.AddFilesPersistence(config.GetConnectionString())
			.AddIdempotencyPersistence(config.GetConnectionString())
			.AddNotificationsPersistence(config.GetConnectionString())
			.AddPrintingPersistence(config.GetConnectionString());

	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddScoped<IPrintCalculator, PrintCalculator>();

		return services;
	}

	public static async Task ExecuteDbMigrationUpdaterAsync(this IServiceCollection services)
	{
		using IServiceScope scope = services.BuildServiceProvider().CreateScope();
		IServiceProvider provider = scope.ServiceProvider;

		await Task.WhenAll([
			provider.UpdateAccountsContextAsync(),
			provider.UpdateCartsContextAsync(),
			provider.UpdateCatalogContextAsync(),
			provider.UpdateCustomsContextAsync(),
			provider.UpdateDeliveryContextAsync(),
			provider.UpdateFilesContextAsync(),
			provider.UpdateIdempotencyContextAsync(),
			provider.UpdateNotificationsContextAsync(),
			provider.UpdatePrintingContextAsync(),
		]).ConfigureAwait(false);
	}
}
