namespace CustomCADs.Notifications.API.Notifications.Endpoints.Get.Stats;

public sealed record GetNotificationsStatsResponse(
	int Unread,
	int Read,
	int Opened,
	int Hidden
);
