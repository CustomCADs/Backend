using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetStatuses;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Get.Statuses;

public class GetNotificationStatusesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<NotificationStatus[]>
{
	public override void Configure()
	{
		Get("statuses");
		Group<NotificationsGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Status")
			.WithDescription("See all Notification statuses")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		NotificationStatus[] response = await sender.SendQueryAsync(
			query: new GetNotificationStatusesQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
