using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.DuplicateByIds;

public sealed class DuplicateCadsByIdsHandler(
	ICadReads reads,
	IWrites<Cad> writes,
	IUnitOfWork uow,
	BaseCachingService<CadId, Cad> cache
) : ICommandHandler<DuplicateCadsByIdsCommand, Dictionary<CadId, CadId>>
{
	public async Task<Dictionary<CadId, CadId>> Handle(DuplicateCadsByIdsCommand req, CancellationToken ct)
	{
		Result<Cad> result = await reads.AllAsync(
			query: new(
				Pagination: new(1, req.Ids.Length),
				Ids: req.Ids
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		Dictionary<CadId, Cad> cads = result.Items.ToDictionary(
			x => x.Id,
			x => Cad.Create(
				key: x.Key,
				contentType: x.ContentType,
				volume: x.Volume,
				camCoordinates: x.CamCoordinates,
				panCoordinates: x.PanCoordinates
			)
		);
		await writes.AddRangeAsync(cads.Values, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		foreach (Cad cad in cads.Values)
		{
			await cache.UpdateAsync(cad.Id, cad).ConfigureAwait(false);
		}

		return cads.ToDictionary(
			x => x.Key,
			x => x.Value.Id
		);
	}
}
