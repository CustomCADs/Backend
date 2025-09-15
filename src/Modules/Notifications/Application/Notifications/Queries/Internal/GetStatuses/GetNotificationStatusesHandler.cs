using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetStatuses;

public sealed class GetNotificationStatusesHandler : IQueryHandler<GetNotificationStatusesQuery, NotificationStatus[]>
{
	public Task<NotificationStatus[]> Handle(GetNotificationStatusesQuery req, CancellationToken ct = default)
		=> Task.FromResult(
			Enum.GetValues<NotificationStatus>()
		);
}
