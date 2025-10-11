using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Internal.Edit;

public sealed class EditCadHandler(
	ICadReads reads,
	IUnitOfWork uow,
	BaseCachingService<CadId, Cad> cache
) : ICommandHandler<EditCadCommand>
{
	public async Task Handle(EditCadCommand req, CancellationToken ct = default)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		if (cad.OwnerId != req.CallerId)
		{
			throw CustomAuthorizationException<Cad>.ById(req.Id);
		}

		cad.SetContentType(req.ContentType).SetVolume(req.Volume);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(cad.Id, cad).ConfigureAwait(false);
	}
}
