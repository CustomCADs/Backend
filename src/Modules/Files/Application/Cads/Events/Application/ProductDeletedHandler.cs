using CustomCADs.Modules.Files.Application.Cads.Storage;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Files.Application.Cads.Events.Application;

public class ProductDeletedHandler(
	ICadReads reads,
	IWrites<Cad> writes,
	IUnitOfWork uow,
	ICadStorageService storage,
	BaseCachingService<CadId, Cad> cache
) : IEventHandler<ProductDeletedApplicationEvent>
{
	public async Task HandleAsync(ProductDeletedApplicationEvent @event)
	{
		Cad cad = await reads.SingleByIdAsync(@event.CadId, track: true).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(@event.CadId);

		await storage.DeleteFileAsync(cad.Key).ConfigureAwait(false);

		writes.Remove(cad);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await cache.ClearAsync(@event.CadId).ConfigureAwait(false);
	}
}
