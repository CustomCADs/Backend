using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.Querying;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Shared.Persistence.Extensions;

public static class PersistenceExtensions
{
	extension<TEntity>(DbSet<TEntity> set) where TEntity : BaseAggregateRoot
	{
		public IQueryable<TEntity> WithTracking(bool track)
			=> track ? set : set.AsNoTracking();
	}

	extension<TEntity>(IQueryable<TEntity> query)
	{
		public IQueryable<TEntity> WithPagination(Pagination pagination)
			=> query
				.Skip(pagination.Limit * (pagination.Page - 1))
				.Take(pagination.Limit);
	}
}
