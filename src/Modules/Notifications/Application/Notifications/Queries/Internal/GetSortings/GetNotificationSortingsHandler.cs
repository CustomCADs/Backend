using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetSortings;

public sealed class GetNotificationSortingsHandler : IQueryHandler<GetNotificationSortingsQuery, NotificationSortingType[]>
{
	public Task<NotificationSortingType[]> Handle(GetNotificationSortingsQuery req, CancellationToken ct = default)
		=> Task.FromResult(
			Enum.GetValues<NotificationSortingType>()
		);
}
