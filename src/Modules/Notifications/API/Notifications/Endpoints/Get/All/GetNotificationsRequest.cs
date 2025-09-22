using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.API.Notifications.Endpoints.Get.All;

public record GetNotificationsRequest(
	NotificationStatus? Status = null,
	NotificationSortingType SortingType = NotificationSortingType.CreatedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 50
);
