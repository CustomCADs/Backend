using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Modules.Notifications.Persistence;
using CustomCADs.Modules.Notifications.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using NotificationReads = CustomCADs.Modules.Notifications.Persistence.Repositories.Notifications.Reads;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateNotificationsContextAsync()
		{
			NotificationsContext context = provider.GetRequiredService<NotificationsContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddNotificationsPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<NotificationsContext>(options =>
				options.UseNpgsql(connectionString, opt
					=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Notifications)
				)
			);

		public IServiceCollection AddReads()
			=> services.AddScoped<INotificationReads, NotificationReads>();

		public IServiceCollection AddWrites()
			=> services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		public IServiceCollection AddUnitOfWork()
			=> services.AddScoped<IUnitOfWork, UnitOfWork>();
	}

}
