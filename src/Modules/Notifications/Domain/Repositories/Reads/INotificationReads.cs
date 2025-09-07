using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Notifications.Domain.Repositories.Reads;

public interface INotificationReads
{
	Task<Result<Notification>> AllAsync(NotificationQuery query, bool track = false, CancellationToken ct = default);
	Task<Notification?> SingleByIdAsync(NotificationId id, bool track = false, CancellationToken ct = default);
	Task<int> CountAsync(NotificationStatus? status = null, CancellationToken ct = default);
}
