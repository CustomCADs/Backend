using CustomCADs.Notifications.Application.Notifications.Queries.Internal;
using CustomCADs.Notifications.Endpoints.Notifications.Endpoints.Get;

namespace CustomCADs.Notifications.Endpoints.Notifications;

internal static class Mapper
{
	internal static GetNotificationsResponse ToResponse(this GetAllNotificationsDto notification)
		=> new(
			Id: notification.Id.Value,
			Type: notification.Type,
			Status: notification.Status,
			CreatedAt: notification.CreatedAt,
			Author: notification.Author,
			Link: notification.Link
		);
}
