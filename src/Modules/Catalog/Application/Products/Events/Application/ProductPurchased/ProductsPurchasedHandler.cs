using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductPurchased;

public sealed class ProductsPurchasedHandler(IUnitOfWork uow)
	: IEventHandler<ProductsPurchasedApplicationEvent>
{
	public async Task HandleAsync(ProductsPurchasedApplicationEvent req)
	{
		await uow.AddProductsPurchasesAsync(req.Ids, count: 1).ConfigureAwait(false);
	}
}
