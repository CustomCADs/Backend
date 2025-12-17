using CustomCADs.Modules.Notifications.Domain.Notifications;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Notifications.Persistence.Repositories.Notifications;

internal static class Utilities
{
	extension(IQueryable<Notification> query)
	{
		internal IQueryable<Notification> WithFilter(AccountId? receiverId = null, NotificationStatus? status = null)
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

		internal IQueryable<Notification> WithSorting(Sorting<NotificationSortingType>? sorting = null)
			=> sorting?.Type switch
			{
				NotificationSortingType.CreatedAt => query.ToSorted(sorting, x => x.CreatedAt),
				NotificationSortingType.Status => query.ToSorted(sorting, x => x.Status),
				_ => query,
			};
	}

}
