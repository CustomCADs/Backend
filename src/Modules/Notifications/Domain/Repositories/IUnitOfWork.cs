using CustomCADs.Notifications.Domain.Notifications;

namespace CustomCADs.Notifications.Domain.Repositories;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken ct = default);
	Task<ICollection<Notification>> InsertNotificationsAsync(ICollection<Notification> notifications, CancellationToken ct = default);
}
