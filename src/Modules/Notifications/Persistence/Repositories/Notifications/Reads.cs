using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Notifications;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Persistence.Repositories.Notifications;

public sealed class Reads(NotificationsContext context) : INotificationReads
{
	public async Task<Result<Notification>> AllAsync(NotificationQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Notification> queryable = context.Notifications
			.WithTracking(track)
			.WithFilter(query.ReceiverId, query.Status);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Notification[] notifications = await queryable
			.WithSorting(query.Sorting ?? new())
			.WithPagination(query.Pagination)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, notifications);
	}

	public async Task<Notification?> SingleByIdAsync(NotificationId id, bool track = true, CancellationToken ct = default)
		=> await context.Notifications
			.WithTracking(track)
			.FirstOrDefaultAsync(s => s.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<NotificationStatus, int>> CountByStatusAsync(AccountId receiverId, CancellationToken ct = default)
		=> await context.Notifications
			.WithTracking(false)
			.Where(x => x.ReceiverId == receiverId)
			.GroupBy(x => x.Status)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
