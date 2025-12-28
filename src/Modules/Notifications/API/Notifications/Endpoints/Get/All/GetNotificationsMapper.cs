using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Get.All;

public class GetNotificationsMapper : ResponseMapper<GetNotificationsResponse, GetAllNotificationsDto>
{
	public override GetNotificationsResponse FromEntity(GetAllNotificationsDto notification)
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
