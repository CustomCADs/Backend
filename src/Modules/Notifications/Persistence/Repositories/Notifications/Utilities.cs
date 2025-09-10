using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Persistence.Repositories.Notifications;

public static class Utilities
{
	public static IQueryable<Notification> WithFilter(this IQueryable<Notification> query, AccountId? receiverId = null)
	{
		if (receiverId is not null)
		{
			query = query.Where(x => x.ReceiverId == receiverId);
		}

		return query;
	}

	public static IQueryable<Notification> WithSorting(this IQueryable<Notification> query, NotificationSorting sorting)
		=> sorting switch
		{
			{ Type: NotificationSortingType.CreatedAt, Direction: SortingDirection.Ascending } => query.OrderBy(s => s.CreatedAt),
			{ Type: NotificationSortingType.CreatedAt, Direction: SortingDirection.Descending } => query.OrderByDescending(s => s.CreatedAt),
			{ Type: NotificationSortingType.Status, Direction: SortingDirection.Ascending } => query.OrderBy(s => s.Status),
			{ Type: NotificationSortingType.Status, Direction: SortingDirection.Descending } => query.OrderByDescending(s => s.Status),
			_ => query,
		};
}
