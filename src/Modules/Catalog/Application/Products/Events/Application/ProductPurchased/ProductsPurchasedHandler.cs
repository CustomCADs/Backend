using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductPurchased;

public sealed class ProductsPurchasedHandler(IUnitOfWork uow)
{
	public async Task HandleAsync(ProductsPurchasedApplicationEvent req, CancellationToken ct)
	{
		await uow.AddProductsPurchasesAsync(req.Ids, count: 1, ct: ct).ConfigureAwait(false);
	}
}
