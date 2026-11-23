using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductCreated;

public class ProductCreatedHandler(IProductWrites writes, IUnitOfWork uow)
	: IEventHandler<ProductCreatedApplicationEvent>
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
