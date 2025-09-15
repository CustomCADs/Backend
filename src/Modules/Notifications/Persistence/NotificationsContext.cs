using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Notifications.Persistence;

using static PersistenceConstants;

public class NotificationsContext(DbContextOptions<NotificationsContext> opts) : DbContext(opts)
{
	public required DbSet<Notification> Notifications { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema(Schemes.Notifications);
		builder.ApplyConfigurationsFromAssembly(NotificationsPersistenceReference.Assembly);
	}
}
