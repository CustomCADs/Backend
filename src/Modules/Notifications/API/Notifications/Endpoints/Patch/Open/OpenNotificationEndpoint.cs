using CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Open;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Patch.Open;

public class OpenNotificationEndpoint(IRequestSender sender)
	: Endpoint<OpenNotificationRequest>
{
	public override void Configure()
	{
		Patch("open");
		Group<NotificationsGroup>();
		Description(x => x
			.WithSummary("Open")
			.WithDescription("Open your Notification")
		);
	}

	public override async Task HandleAsync(OpenNotificationRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new OpenNotificationCommand(
				Id: NotificationId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
