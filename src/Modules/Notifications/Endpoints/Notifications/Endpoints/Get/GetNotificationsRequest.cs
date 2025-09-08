using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Get;

public record GetNotificationsRequest(
	Guid ReceiverId,
	NotificationSortingType SortingType = NotificationSortingType.CreatedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 50
);
