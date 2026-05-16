using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;

public class ProductDeletedHandler(IUnitOfWork uow)
{
	public async Task HandleAsync(ProductDeletedApplicationEvent @event)
	{
		await uow.BulkDeleteItemsByProductIdAsync(@event.Id).ConfigureAwait(false);
	}
}
