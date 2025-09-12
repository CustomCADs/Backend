using CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetStatuses;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Get.Statuses;

public class GetNotificationStatusesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<NotificationStatus[]>
{
	public override void Configure()
	{
		Get("statuses");
		Group<NotificationsGroup>();
		AllowAnonymous();
		Description(d => d
			.WithSummary("Status")
			.WithDescription("See all Notification statuses")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		NotificationStatus[] response = await sender.SendQueryAsync(
			new GetNotificationStatusesQuery(),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
