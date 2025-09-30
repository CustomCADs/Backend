using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.Catalog.Application.Products.Events.Application.ProductCreated;

public class ProductCreatedHandler(IProductWrites writes, IUnitOfWork uow)
{
	public async Task Handle(ProductCreatedApplicationEvent ae)
	{
		foreach (TagId tagId in ae.TagIds)
		{
			await writes.AddTagAsync(ae.Id, tagId).ConfigureAwait(false);
		}

		await uow.SaveChangesAsync().ConfigureAwait(false);

	}
}
