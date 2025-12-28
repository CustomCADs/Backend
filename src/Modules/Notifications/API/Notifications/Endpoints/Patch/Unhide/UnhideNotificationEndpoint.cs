using CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Unhide;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Patch.Unhide;

public class UnhideNotificationEndpoint(IRequestSender sender)
	: Endpoint<UnhideNotificationRequest>
{
	public override void Configure()
	{
		Patch("unhide");
		Group<NotificationsGroup>();
		Description(x => x
			.WithSummary("Unhide")
			.WithDescription("Unhide your Notification")
		);
	}

	public override async Task HandleAsync(UnhideNotificationRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new UnhideNotificationCommand(
				Id: NotificationId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
