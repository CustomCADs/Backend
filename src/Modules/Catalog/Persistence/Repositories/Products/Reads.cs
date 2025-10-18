using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Persistence.Extensions;

namespace CustomCADs.Catalog.Persistence.Repositories.Products;

public sealed class Reads(CatalogContext context) : IProductReads
{
	public async Task<Result<Product>> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default)
	{
		ProductId[]? ids = await context.ProductTags
			.GetProductIdsByTagIdsOrDefaultAsync(query.TagIds, ct)
			.ConfigureAwait(false)
			?? query.Ids;

		IQueryable<Product> queryable = context.Products
				.WithTracking(track)
				.WithFilter(ids, query.CreatorId, query.DesignerId, query.CategoryId, query.Status)
				.WithSearch(query.Name);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		Product[] products = await queryable
				.WithSorting(query.Sorting)
				.WithPagination(query.Pagination)
				.ToArrayAsync(ct)
				.ConfigureAwait(false);

		return new(count, products);
	}

	public async Task<ProductId[]> AllAsync(DateTimeOffset? before, DateTimeOffset? after, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(false)
			.WithFilter(before: before, after: after)
			.Select(x => x.Id)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Product?> OldestByTagAsync(string tag, CancellationToken ct = default)
		=> await context.ProductTags
			.Include(x => x.Product)
			.Include(x => x.Tag)
			.Where(x => x.Tag.Name == tag)
			.OrderBy(x => x.Product.UploadedAt)
			.Select(x => x.Product)
			.FirstOrDefaultAsync(ct)
			.ConfigureAwait(false);

	public async Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Product?> SingleByCadIdAsync(CadId cadId, bool track = true, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.CadId == cadId, ct)
			.ConfigureAwait(false);

	public async Task<string[]> TagsByIdAsync(ProductId id, CancellationToken ct = default)
		=> await context.ProductTags
			.Where(x => x.ProductId == id)
			.Select(x => x.Tag.Name)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<ProductId, string[]>> TagsByIdsAsync(ProductId[] ids, CancellationToken ct = default)
		=> await context.Products
			.GroupJoin(
				context.ProductTags,
				x => x.Id,
				x => x.ProductId,
				(product, productTags) => new
				{
					Id = product.Id,
					Tags = productTags.Select(x => x.Tag.Name).ToArray()
				}
			)
			.ToDictionaryAsync(x => x.Id, x => x.Tags, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<ProductStatus, int>> CountByStatusAsync(AccountId creatorId, CancellationToken ct = default)
		=> await context.Products
			.WithTracking(false)
			.Where(x => x.CreatorId == creatorId)
			.GroupBy(x => x.Status)
			.ToDictionaryAsync(x => x.Key, x => x.Count(), ct)
			.ConfigureAwait(false);
}
