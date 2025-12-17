using CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Hide;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Patch.Hide;

public class HideNotificationEndpoint(IRequestSender sender)
	: Endpoint<HideNotificationRequest>
{
	public override void Configure()
	{
		Patch("hide");
		Group<NotificationsGroup>();
		Description(x => x
			.WithSummary("Hide")
			.WithDescription("Hide your Notification")
		);
	}

	public override async Task HandleAsync(HideNotificationRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new HideNotificationCommand(
				Id: NotificationId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
