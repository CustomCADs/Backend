using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.ValueObjects;
using System.Linq.Expressions;

namespace CustomCADs.Shared.Domain.Extensions;

public static class DomainExtensions
{
	extension<TOldItem>(Result<TOldItem> result)
	{
		public Result<TNewItem> ToNewResult<TNewItem>(Func<TOldItem, TNewItem> selector)
			=> new(
				Count: result.Count,
				Items: [.. result.Items.Select(selector)]
			);
	}

	extension<TEntity>(IQueryable<TEntity> query) where TEntity : BaseEntity
	{
		public IQueryable<TEntity> ToSorted<TSorting, TKey>(Sorting<TSorting> sorting, Expression<Func<TEntity, TKey>> selector) where TSorting : Enum
			=> sorting.Direction switch
			{
				SortingDirection.Ascending => query.OrderBy(selector),
				SortingDirection.Descending => query.OrderByDescending(selector),
				_ => query,
			};
	}
}
