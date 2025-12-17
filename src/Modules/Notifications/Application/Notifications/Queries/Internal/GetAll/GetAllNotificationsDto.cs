using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Application.Dtos.Notifications;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;

public sealed record GetAllNotificationsDto(
	NotificationId Id,
	NotificationType Type,
	NotificationStatus Status,
	DateTimeOffset CreatedAt,
	string Author,
	string Description,
	string? Link
);
