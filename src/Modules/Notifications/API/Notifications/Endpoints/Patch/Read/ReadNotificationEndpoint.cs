using CustomCADs.Notifications.Application.Notifications.Commands.Internal.Read;

namespace CustomCADs.Notifications.API.Notifications.Endpoints.Patch.Read;

public class ReadNotificationEndpoint(IRequestSender sender)
	: Endpoint<ReadNotificationRequest>
{
	public override void Configure()
	{
		Patch("read");
		Group<NotificationsGroup>();
		Description(x => x
			.WithSummary("Read")
			.WithDescription("Read your Notification")
		);
	}

	public override async Task HandleAsync(ReadNotificationRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ReadNotificationCommand(
				Id: NotificationId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
