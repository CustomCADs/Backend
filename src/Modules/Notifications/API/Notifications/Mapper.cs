using CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetAll;
using CustomCADs.Notifications.API.Notifications.Endpoints.Get.All;

namespace CustomCADs.Notifications.API.Notifications;

internal static class Mapper
{
	internal static GetNotificationsResponse ToResponse(this GetAllNotificationsDto notification)
		=> new(
			Id: notification.Id.Value,
			Type: notification.Type,
			Status: notification.Status,
			CreatedAt: notification.CreatedAt,
			Author: notification.Author,
			Description: notification.Description,
			Link: notification.Link
		);
}
