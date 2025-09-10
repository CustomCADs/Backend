using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Get.All;

public record GetNotificationsResponse(
	Guid Id,
	NotificationType Type,
	NotificationStatus Status,
	DateTimeOffset CreatedAt,
	string Author,
	string Description,
	string? Link
);
