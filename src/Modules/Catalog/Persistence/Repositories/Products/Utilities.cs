using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Persistence.ShadowEntities;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Catalog.Persistence.Repositories.Products;

public static class Utilities
{
	public static IQueryable<Product> WithFilter(
		this IQueryable<Product> query,
		ProductId[]? ids = null,
		AccountId? creatorId = null,
		AccountId? designerId = null,
		CategoryId? categoryId = null,
		ProductStatus? status = null,
		DateTimeOffset? before = null,
		DateTimeOffset? after = null
	)
	{
		if (ids is not null)
		{
			query = query.Where(x => ids.Contains(x.Id));
		}
		if (creatorId is not null)
		{
			query = query.Where(x => x.CreatorId == creatorId);
		}
		if (designerId is not null)
		{
			query = query.Where(x => x.DesignerId == designerId);
		}
		if (categoryId is not null)
		{
			query = query.Where(x => x.CategoryId == categoryId);
		}
		if (status is not null)
		{
			query = query.Where(x => x.Status == status);
		}
		if (before is not null)
		{
			query = query.Where(x => x.UploadedAt <= before);
		}
		if (after is not null)
		{
			query = query.Where(x => x.UploadedAt >= after);
		}

		return query;
	}

	public static IQueryable<Product> WithSearch(this IQueryable<Product> query, string? name = null)
	{
		if (!string.IsNullOrWhiteSpace(name))
		{
			query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
		}

		return query;
	}

	public static IQueryable<Product> WithSorting(this IQueryable<Product> query, Sorting<ProductSortingType>? sorting = null)
		=> sorting?.Type switch
		{
			ProductSortingType.UploadedAt => query.ToSorted(sorting, x => x.UploadedAt),
			ProductSortingType.Alphabetical => query.ToSorted(sorting, x => x.Name),
			ProductSortingType.Status => query.ToSorted(sorting, x => (int)x.Status),
			ProductSortingType.Cost => query.ToSorted(sorting, x => x.Price),
			ProductSortingType.Purchases => query.ToSorted(sorting, x => x.Counts.Purchases),
			ProductSortingType.Views => query.ToSorted(sorting, x => x.Counts.Views),
			_ => query,
		};

	public static async Task<ProductId[]?> GetProductIdsByTagIdsOrDefaultAsync(this DbSet<ProductTag> set, TagId[]? tagIds, CancellationToken ct = default)
		=> tagIds is not null
			? await set
				.Where(x => tagIds.Contains(x.TagId))
				.GroupBy(x => x.ProductId)
				.Where(x => x.Count() == tagIds.Length)
				.Select(x => x.Key)
				.ToArrayAsync(ct)
				.ConfigureAwait(false)
			: null;
}
