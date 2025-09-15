using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.ValueObjects;
using System.Linq.Expressions;

namespace CustomCADs.Shared.Domain;

public static class DomainExtensions
{
	public static Result<TNewItem> ToNewResult<TOldItem, TNewItem>(
		this Result<TOldItem> result,
		Func<TOldItem, TNewItem> selector
	) => new(
		Count: result.Count,
		Items: [.. result.Items.Select(selector)]
	);

	public static IQueryable<TEntity> ToSorted<TEntity, TSorting, TKey>(
		this IQueryable<TEntity> query,
		Sorting<TSorting> sorting,
		Expression<Func<TEntity, TKey>> selector
	) where TEntity : BaseEntity where TSorting : Enum
		=> sorting.Direction switch
		{
			SortingDirection.Ascending => query.OrderBy(selector),
			SortingDirection.Descending => query.OrderByDescending(selector),
			_ => query,
		};
}
