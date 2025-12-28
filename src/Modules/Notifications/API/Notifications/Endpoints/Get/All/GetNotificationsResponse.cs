using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Application.Dtos.Notifications;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Get.All;

public record GetNotificationsResponse(
	Guid Id,
	NotificationType Type,
	NotificationStatus Status,
	DateTimeOffset CreatedAt,
	string Author,
	string Description,
	string? Link
);
