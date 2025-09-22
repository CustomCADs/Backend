using CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetSortings;
using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Notifications.API.Notifications.Endpoints.Get.Sortings;

public class GetNotificationSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<NotificationSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<NotificationsGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Notification Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		NotificationSortingType[] response = await sender.SendQueryAsync(
			query: new GetNotificationSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
