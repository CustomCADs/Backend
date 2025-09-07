using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Notifications;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Notifications.Persistence.Repositories.Notifications;

public sealed class Reads(NotificationsContext context) : INotificationReads
{
	public async Task<Result<Notification>> AllAsync(NotificationQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<Notification> queryable = context.Notifications
			.WithTracking(track)
			.WithFilter(query.ReceiverId);

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

	public async Task<int> CountAsync(NotificationStatus? status, CancellationToken ct = default)
		=> await context.Notifications
			.WithTracking(false)
			.CountAsync(ct)
			.ConfigureAwait(false);
}
