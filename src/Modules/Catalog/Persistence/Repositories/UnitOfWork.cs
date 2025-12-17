using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Shared.Persistence.Exceptions;

namespace CustomCADs.Modules.Catalog.Persistence.Repositories;

public class UnitOfWork(CatalogContext context) : IUnitOfWork
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

	public async Task ClearProductTagsAsync(ProductId[] ids, string tag, CancellationToken ct = default)
		=> await context.ProductTags
			.Include(x => x.Tag)
			.Where(x => ids.Contains(x.ProductId))
			.Where(x => x.Tag.Name == tag)
			.ExecuteDeleteAsync(ct)
			.ConfigureAwait(false);

	public async Task AddProductPurchasesAsync(ProductId[] ids, int count = 1, CancellationToken ct = default)
		=> await context.Products
			.Where(x => ids.Contains(x.Id))
			.ExecuteUpdateAsync(x => x
				.SetProperty(
					x => x.Counts.Purchases,
					x => x.Counts.Purchases + count
				),
				cancellationToken: ct
			).ConfigureAwait(false);
}
