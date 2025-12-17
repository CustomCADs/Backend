using CustomCADs.Modules.Notifications.Application.Contracts;
using CustomCADs.Modules.Notifications.Infrastructure.Notifiers;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddNotificationsRealTimeNotifier()
			=> services.AddScoped<INotificationsRealTimeNotifier, SignalRNotificationsRealTimeNotifier>();
	}
}
