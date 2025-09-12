using CustomCADs.Notifications.Application.Contracts;
using CustomCADs.Notifications.Infrastructure.Notifiers;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddNotificationsRealTimeNotifier(this IServiceCollection services)
	{
		services.AddScoped<INotificationsRealTimeNotifier, SignalRNotificationsRealTimeNotifier>();

		return services;
	}
}
