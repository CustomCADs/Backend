using CustomCADs.Notifications.Application.Contracts;
using CustomCADs.Notifications.Infrastructure.Hubs;
using CustomCADs.Shared.Infrastructure.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace CustomCADs.Notifications.Infrastructure.Notifiers;

public class SignalRNotificationsRealTimeNotifier(
	IHubContext<SignalRNotificationsHub> hub
) : SignalRRealTimeNotifier<SignalRNotificationsHub>(hub),
	INotificationsRealTimeNotifier;
