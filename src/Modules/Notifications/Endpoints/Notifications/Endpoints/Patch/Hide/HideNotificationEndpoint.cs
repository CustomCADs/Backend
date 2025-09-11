using CustomCADs.Notifications.Application.Notifications.Commands.Internal.Hide;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Patch.Hide;

public class HideNotificationEndpoint(IRequestSender sender)
	: Endpoint<HideNotificationRequest>
{
	public override void Configure()
	{
		Patch("hide");
		Group<NotificationsGroup>();
		Description(d => d
			.WithSummary("Hide")
			.WithDescription("Hide your Notification")
		);
	}

	public override async Task HandleAsync(HideNotificationRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new HideNotificationCommand(
				Id: NotificationId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
