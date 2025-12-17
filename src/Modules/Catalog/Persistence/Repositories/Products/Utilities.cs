using CustomCADs.Modules.Catalog.Domain.Products;
using CustomCADs.Modules.Catalog.Persistence.ShadowEntities;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Catalog.Persistence.Repositories.Products;

internal static class Utilities
{
	extension(IQueryable<Product> query)
	{
		internal IQueryable<Product> WithFilter(
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

		internal IQueryable<Product> WithSearch(string? name = null)
		{
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
			}

			return query;
		}

		internal IQueryable<Product> WithSorting(Sorting<ProductSortingType>? sorting = null)
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
	}

	extension(DbSet<ProductTag> set)
	{
		internal async Task<ProductId[]?> GetProductIdsByTagIdsOrDefaultAsync(TagId[]? tagIds, CancellationToken ct = default)
			=> tagIds is not null
				? await set.GetProductIdsByTagIdsAsync(tagIds, ct).ConfigureAwait(false)
				: null;

		private async Task<ProductId[]> GetProductIdsByTagIdsAsync(TagId[] tagIds, CancellationToken ct = default)
			=> await set
				.Where(x => tagIds.Contains(x.TagId))
				.GroupBy(x => x.ProductId)
				.Where(x => x.Count() == tagIds.Length)
				.Select(x => x.Key)
				.ToArrayAsync(ct)
				.ConfigureAwait(false);
	}

}
