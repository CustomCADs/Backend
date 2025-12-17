using CustomCADs.Modules.Notifications.Domain.Notifications;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Shared.Persistence.Exceptions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Notifications.Persistence.Repositories;

public class UnitOfWork(NotificationsContext context) : IUnitOfWork
{
	public async Task SaveChangesAsync(CancellationToken ct = default)
	{
		try
		{
			await context.SaveChangesAsync(ct).ConfigureAwait(false);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			throw DatabaseConflictException.Custom(ex.Message);
		}
		catch (DbUpdateException ex)
		{
			throw DatabaseException.Custom(ex.Message);
		}
	}

	public async Task<ICollection<Notification>> InsertNotificationsAsync(ICollection<Notification> notifications, CancellationToken ct = default)
	{
		try
		{
			await context.BulkInsertAsync(notifications, cancellationToken: ct).ConfigureAwait(false);

			return await context.Notifications
				.Where(x => notifications.Select(x => x.Id).Contains(x.Id))
				.ToArrayAsync(ct)
				.ConfigureAwait(false);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			throw DatabaseConflictException.Custom(ex.Message);
		}
		catch (DbUpdateException ex)
		{
			throw DatabaseException.Custom(ex.Message);
		}
	}
}
