using Microsoft.AspNetCore.SignalR;

namespace CustomCADs.Notifications.Infrastructure.Hubs;

public class SignalRNotificationsHub : Hub<INotificationsHub>;
