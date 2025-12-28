using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Modules.Files.Domain.Repositories;

public interface IWrites<TEntity> where TEntity : BaseAggregateRoot
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
	Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);
	void Remove(TEntity entity);
}
