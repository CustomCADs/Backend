using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductPurchased;

public sealed class UserPurchasedProductHandler(IUnitOfWork uow)
{
	public async Task HandleAsync(UserPurchasedProductApplicationEvent req, CancellationToken ct)
	{
		await uow.AddProductPurchasesAsync(req.Ids, count: 1, ct: ct).ConfigureAwait(false);
	}
}
