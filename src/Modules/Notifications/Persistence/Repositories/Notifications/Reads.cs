using CustomCADs.Modules.Notifications.Domain.Notifications;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Notifications;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Notifications.Persistence.Repositories.Notifications;

public sealed class Reads(NotificationsContext context) : INotificationReads
{
	public async Task<Result<Notification>> AllAsync(NotificationQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Notification> queryable = context.Notifications
			.WithTracking(track)
			.WithFilter(query.ReceiverId, query.Status);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Notification[] notifications = await queryable
			.WithSorting(query.Sorting)
			.WithPagination(query.Pagination)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, notifications);
	}

	public async Task<Notification?> SingleByIdAsync(NotificationId id, bool track = true, CancellationToken ct = default)
		=> await context.Notifications
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<NotificationStatus, int>> CountByStatusAsync(AccountId receiverId, CancellationToken ct = default)
		=> await context.Notifications
			.WithTracking(false)
			.Where(x => x.ReceiverId == receiverId)
			.GroupBy(x => x.Status)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
