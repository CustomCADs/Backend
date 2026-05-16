using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductCreated;

public class ProductCreatedHandler(IProductWrites writes, IUnitOfWork uow)
{
	public async Task HandleAsync(ProductCreatedApplicationEvent @event)
	{
		foreach (TagId tagId in @event.TagIds)
		{
			await writes.AddTagAsync(@event.Id, tagId).ConfigureAwait(false);
		}

		await uow.SaveChangesAsync().ConfigureAwait(false);

	}
}
