using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Internal.SetCoords;

public sealed class SetCadCoordsHandler(
	ICadReads reads,
	IUnitOfWork uow,
	BaseCachingService<CadId, Cad> cache
) : ICommandHandler<SetCadCoordsCommand>
{
	public async Task Handle(SetCadCoordsCommand req, CancellationToken ct)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		if (cad.OwnerId != req.CallerId)
		{
			throw CustomAuthorizationException<Cad>.ById(req.Id);
		}

		if (req.CamCoordinates is not null)
		{
			cad.SetCamCoordinates(req.CamCoordinates.ToValueObject());
		}

		if (req.PanCoordinates is not null)
		{
			cad.SetPanCoordinates(req.PanCoordinates.ToValueObject());
		}

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
		await cache.UpdateAsync(cad.Id, cad).ConfigureAwait(false);
	}
}
