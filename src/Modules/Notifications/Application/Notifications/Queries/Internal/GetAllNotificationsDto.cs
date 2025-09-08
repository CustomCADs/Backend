using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal;

public record GetAllNotificationsDto(
	NotificationId Id,
	NotificationType Type,
	NotificationStatus Status,
	DateTimeOffset CreatedAt,
	string Author,
	string? Link
);
