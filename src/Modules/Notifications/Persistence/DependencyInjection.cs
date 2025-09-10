using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Notifications.Persistence.Repositories;
using CustomCADs.Notifications.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using NotificationReads = CustomCADs.Notifications.Persistence.Repositories.Notifications.Reads;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateNotificationsContextAsync(this IServiceProvider provider)
	{
		NotificationsContext context = provider.GetRequiredService<NotificationsContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	public static IServiceCollection AddNotificationsPersistence(this IServiceCollection services, IConfiguration config)
		=> services
			.AddContext(config)
			.AddReads()
			.AddWrites()
			.AddUnitOfWork();

	public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
	{
		string connectionString = config.GetConnectionString("ApplicationConnection")
			?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

		services.AddDbContext<NotificationsContext>(options =>
			options.UseNpgsql(connectionString, opt =>
				opt.MigrationsHistoryTable("__EFMigrationsHistory", "Notifications")
			)
		);

		return services;
	}

	public static IServiceCollection AddReads(this IServiceCollection services)
	{
		services.AddScoped<INotificationReads, NotificationReads>();

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
