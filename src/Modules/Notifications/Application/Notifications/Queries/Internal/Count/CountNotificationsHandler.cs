using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.Count;

public sealed class CountNotificationsHandler(INotificationReads reads)
	: IQueryHandler<CountNotificationsQuery, CountNotificationsDto>
{
	public async Task<CountNotificationsDto> Handle(CountNotificationsQuery req, CancellationToken ct)
	{
		Dictionary<NotificationStatus, int> counts = await reads
			.CountByStatusAsync(req.ReceiverId, ct: ct)
			.ConfigureAwait(false);

		return new(
			Unread: TryGetCount(counts, NotificationStatus.Unread),
			Read: TryGetCount(counts, NotificationStatus.Read),
			Opened: TryGetCount(counts, NotificationStatus.Opened),
			Hidden: TryGetCount(counts, NotificationStatus.Hidden)
		);
	}

	private static int TryGetCount(Dictionary<NotificationStatus, int> counts, NotificationStatus status)
	{
		if (counts.TryGetValue(status, out int count))
		{
			return count;
		}

		return 0;
	}
}
