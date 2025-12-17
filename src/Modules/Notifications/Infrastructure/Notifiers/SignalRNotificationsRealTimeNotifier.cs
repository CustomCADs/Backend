using CustomCADs.Modules.Notifications.Application.Contracts;
using CustomCADs.Modules.Notifications.Infrastructure.Hubs;
using CustomCADs.Shared.Infrastructure.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace CustomCADs.Modules.Notifications.Infrastructure.Notifiers;

public class SignalRNotificationsRealTimeNotifier(
	IHubContext<SignalRNotificationsHub> hub
) : SignalRRealTimeNotifier<SignalRNotificationsHub>(hub),
	INotificationsRealTimeNotifier;
