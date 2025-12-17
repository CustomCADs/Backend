using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.Count;

namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints.Get.Stats;

public class GetNotificationsStatsMapper : ResponseMapper<GetNotificationsStatsResponse, CountNotificationsDto>
{
	public override GetNotificationsStatsResponse FromEntity(CountNotificationsDto counts)
		=> new(
			Unread: counts.Unread,
			Read: counts.Read,
			Opened: counts.Opened,
			Hidden: counts.Hidden
		);

}
