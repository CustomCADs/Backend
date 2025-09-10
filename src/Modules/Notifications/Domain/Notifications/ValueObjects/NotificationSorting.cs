using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Domain.Notifications.ValueObjects;

public record NotificationSorting(
	NotificationSortingType Type = NotificationSortingType.CreatedAt,
	SortingDirection Direction = SortingDirection.Descending
);
