using CustomCADs.Modules.Printing.Domain.Services;
using CustomCADs.Shared.Persistence;
using CustomCADs.Shared.Persistence.Exceptions;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;

public static class ProgramExtensions
{
	extension(IConfiguration config)
	{
		internal string ConnectionString => config.GetConnectionString(ConnectionStringKey) ?? throw DatabaseConnectionException.Missing(ConnectionStringKey);
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddPersistence(IConfiguration config)
			=> services
				.AddAccountsPersistence(config.ConnectionString)
				.AddCartsPersistence(config.ConnectionString)
				.AddCatalogPersistence(config.ConnectionString)
				.AddCustomsPersistence(config.ConnectionString)
				.AddDeliveryPersistence(config.ConnectionString)
				.AddFilesPersistence(config.ConnectionString)
				.AddIdempotencyPersistence(config.ConnectionString)
				.AddNotificationsPersistence(config.ConnectionString)
				.AddPrintingPersistence(config.ConnectionString);

		public IServiceCollection AddDomainServices()
		{
			services.AddScoped<IPrintCalculator, PrintCalculator>();

			return services;
		}

		public async Task ExecuteDbMigrationUpdaterAsync()
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

}
