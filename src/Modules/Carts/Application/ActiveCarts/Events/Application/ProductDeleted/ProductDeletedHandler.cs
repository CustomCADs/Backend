using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Files;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;

public class ProductDeletedHandler(IUnitOfWork uow)
{
	public async Task HandleAsync(ProductDeletedApplicationEvent ae)
	{
		await uow.BulkDeleteItemsByProductIdAsync(ae.Id).ConfigureAwait(false);
	}
}
