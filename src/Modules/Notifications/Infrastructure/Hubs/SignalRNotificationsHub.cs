using Microsoft.AspNetCore.SignalR;

namespace CustomCADs.Modules.Notifications.Infrastructure.Hubs;

public class SignalRNotificationsHub : Hub<INotificationsHub>;
