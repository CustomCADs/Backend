using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Domain.Repositories.Reads;

public interface INotificationReads
{
	Task<Result<Notification>> AllAsync(NotificationQuery query, bool track = true, CancellationToken ct = default);
	Task<Notification?> SingleByIdAsync(NotificationId id, bool track = true, CancellationToken ct = default);
	Task<Dictionary<NotificationStatus, int>> CountByStatusAsync(AccountId receiverId, CancellationToken ct = default);
}
