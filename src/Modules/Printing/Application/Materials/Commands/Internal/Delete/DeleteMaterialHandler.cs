using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Delete;

public sealed class DeleteMaterialHandler(
	IMaterialReads reads,
	IWrites<Material> writes,
	IUnitOfWork uow,
	BaseCachingService<MaterialId, Material> cache
) : ICommandHandler<DeleteMaterialCommand>
{
	public async Task Handle(DeleteMaterialCommand req, CancellationToken ct)
	{
		Material material = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(req.Id);

		writes.Remove(material);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.ClearAsync(req.Id).ConfigureAwait(false);
	}
}
