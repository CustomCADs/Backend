using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Extensions;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.Count;

public sealed class CountNotificationsHandler(INotificationReads reads)
	: IQueryHandler<CountNotificationsQuery, CountNotificationsDto>
{
	public async Task<CountNotificationsDto> Handle(CountNotificationsQuery req, CancellationToken ct)
	{
		Dictionary<NotificationStatus, int> counts = await reads
			.CountByStatusAsync(req.CallerId, ct: ct)
			.ConfigureAwait(false);

		return new(
			Unread: counts.GetCountOrZero(NotificationStatus.Unread),
			Read: counts.GetCountOrZero(NotificationStatus.Read),
			Opened: counts.GetCountOrZero(NotificationStatus.Opened),
			Hidden: counts.GetCountOrZero(NotificationStatus.Hidden)
		);
	}
}
