using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Notifications.Persistence.Repositories.Notifications;

public static class Utilities
{
	public static IQueryable<Notification> WithFilter(this IQueryable<Notification> query, AccountId? receiverId = null, NotificationStatus? status = null)
	{
		if (receiverId is not null)
		{
			query = query.Where(x => x.ReceiverId == receiverId);
		}
		if (status is not null)
		{
			query = query.Where(x => x.Status == status);
		}

		return query;
	}

	public static IQueryable<Notification> WithSorting(this IQueryable<Notification> query, Sorting<NotificationSortingType>? sorting = null)
		=> sorting?.Type switch
		{
			NotificationSortingType.CreatedAt => query.ToSorted(sorting, x => x.CreatedAt),
			NotificationSortingType.Status => query.ToSorted(sorting, x => x.Status),
			_ => query,
		};
}
