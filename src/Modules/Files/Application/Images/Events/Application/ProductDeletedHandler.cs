using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Files.Application.Images.Events.Application;

public class ProductDeletedHandler(
	IImageReads reads,
	IWrites<Image> writes,
	IUnitOfWork uow,
	IImageStorageService storage,
	BaseCachingService<ImageId, Image> cache
) : IEventHandler<ProductDeletedApplicationEvent>
{
	public async Task HandleAsync(ProductDeletedApplicationEvent @event)
	{
		Image image = await reads.SingleByIdAsync(@event.ImageId, track: true).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(@event.ImageId);

		await storage.DeleteFileAsync(image.Key).ConfigureAwait(false);

		writes.Remove(image);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await cache.ClearAsync(@event.ImageId).ConfigureAwait(false);
	}
}
