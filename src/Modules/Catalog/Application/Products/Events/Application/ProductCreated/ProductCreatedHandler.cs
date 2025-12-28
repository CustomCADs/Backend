using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductCreated;

public class ProductCreatedHandler(IProductWrites writes, IUnitOfWork uow)
{
	public async Task HandleAsync(ProductCreatedApplicationEvent ae)
	{
		foreach (TagId tagId in ae.TagIds)
		{
			await writes.AddTagAsync(ae.Id, tagId).ConfigureAwait(false);
		}

		await uow.SaveChangesAsync().ConfigureAwait(false);

	}
}
