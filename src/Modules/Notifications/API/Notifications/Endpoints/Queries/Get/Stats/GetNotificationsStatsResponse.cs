namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Queries.Get.Stats;

public sealed record GetNotificationsStatsResponse(
	int Unread,
	int Read,
	int Opened,
	int Hidden
);
