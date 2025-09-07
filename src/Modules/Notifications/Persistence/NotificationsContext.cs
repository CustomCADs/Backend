using CustomCADs.Notifications.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Notifications.Persistence;

public class NotificationsContext(DbContextOptions<NotificationsContext> opts) : DbContext(opts)
{
	public required DbSet<Notification> Notifications { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.HasDefaultSchema("Notifications");
		builder.ApplyConfigurationsFromAssembly(NotificationsPersistenceReference.Assembly);
	}
}
