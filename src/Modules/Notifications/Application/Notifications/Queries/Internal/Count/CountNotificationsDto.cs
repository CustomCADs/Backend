namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.Count;

public record CountNotificationsDto(
	int Unread,
	int Read,
	int Opened,
	int Hidden
);
