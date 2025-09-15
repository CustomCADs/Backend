using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Notifications.Domain.Repositories;

public interface IWrites<TEntity> where TEntity : BaseAggregateRoot
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
	void Remove(TEntity entity);
}
