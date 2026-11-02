using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;
using CustomCADs.Shared.Application.Dtos.Notifications;

namespace CustomCADs.Modules.Notifications.Application.Notifications;

internal static class Mapper
{
	extension(Notification notification)
	{
		internal GetAllNotificationsDto ToGetAllDto(string author)
			=> new(
				Id: notification.Id,
				Type: ToType(notification),
				Status: notification.Status,
				CreatedAt: notification.CreatedAt,
				Author: author,
				Description: notification.Content.Description,
				Link: notification.Content.Link
			);

		internal NotificationType ToType()
			=> Enum.TryParse(notification.Type, out NotificationType type) ? type : NotificationType.Unknown;
	}

}
