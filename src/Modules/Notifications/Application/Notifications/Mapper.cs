using CustomCADs.Notifications.Application.Notifications.Queries.Internal;
using CustomCADs.Shared.Application.Dtos.Notifications;

namespace CustomCADs.Notifications.Application.Notifications;

internal static class Mapper
{
	internal static GetAllNotificationsDto ToGetAllDto(this Notification notification, string author)
		=> new(
			Id: notification.Id,
			Type: ToType(notification),
			Status: notification.Status,
			CreatedAt: notification.CreatedAt,
			Author: author,
			Link: notification.Content.Link
		);

	internal static NotificationType ToType(this Notification notification)
		=> notification.Type switch
		{
			nameof(NotificationType.CustomAccepted) => NotificationType.CustomAccepted,
			nameof(NotificationType.CustomBegun) => NotificationType.CustomBegun,
			nameof(NotificationType.CustomFinished) => NotificationType.CustomFinished,
			nameof(NotificationType.CustomCompleted) => NotificationType.CustomCompleted,
			nameof(NotificationType.CustomReported) => NotificationType.CustomReported,
			nameof(NotificationType.ProductValidated) => NotificationType.ProductValidated,
			nameof(NotificationType.ProductReported) => NotificationType.ProductReported,
			nameof(NotificationType.ProductRemoved) => NotificationType.ProductRemoved,
			nameof(NotificationType.PaymentCompleted) => NotificationType.PaymentCompleted,
			nameof(NotificationType.PaymentFailed) => NotificationType.PaymentFailed,
			_ => NotificationType.Unkown,
		};
}
