using CustomCADs.Notifications.Application.Notifications.Commands.Internal.Unhide;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Notifications.API.Notifications.Endpoints.Patch.Unhide;

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
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
