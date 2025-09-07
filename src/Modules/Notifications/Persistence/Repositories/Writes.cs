using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Notifications.Persistence.Repositories;

public class Writes<TEntity>(NotificationsContext context) : IWrites<TEntity>
	where TEntity : BaseAggregateRoot
{
	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
		=> (await context.Set<TEntity>().AddAsync(entity, ct).ConfigureAwait(false)).Entity;

	public void Remove(TEntity entity)
		=> context.Set<TEntity>().Remove(entity);
}
