using CustomCADs.Modules.Accounts.Domain.Accounts;
using CustomCADs.Modules.Accounts.Domain.Accounts.Entities;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Accounts.Persistence.Repositories.Accounts;

public class Writes(AccountsContext context) : IAccountWrites
{
	public async Task<Account> AddAsync(Account entity, CancellationToken ct = default)
		=> (await context.Accounts.AddAsync(entity, ct).ConfigureAwait(false)).Entity;

	public async Task ViewProductAsync(AccountId id, ProductId productId, DateTimeOffset viewedAt, CancellationToken ct = default)
		=> await context.ViewedProducts.AddAsync(
				entity: ViewedProduct.Create(id, productId, viewedAt),
				cancellationToken: ct
			).ConfigureAwait(false);

	public async Task UnviewProductAsync(AccountId id, ProductId productId, CancellationToken ct = default)
	{
		ViewedProduct? viewedProduct = await context.ViewedProducts
			.FirstOrDefaultAsync(x => x.AccountId == id && x.ProductId == productId, ct)
			.ConfigureAwait(false);

		if (viewedProduct is not null)
		{
			context.ViewedProducts.Remove(viewedProduct);
		}
	}

	public void Remove(Account entity)
		=> entity.Delete();
}
