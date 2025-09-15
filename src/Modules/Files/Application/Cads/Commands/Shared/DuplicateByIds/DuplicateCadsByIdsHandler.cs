using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.DuplicateByIds;

public sealed class DuplicateCadsByIdsHandler(
	ICadReads reads,
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

		ICollection<Cad> cads = await uow.BulkInsertCadsAsync(
			cads: [..
				result.Items.Select(x => Cad.CreateWithId(
					id: CadId.New(),
					key: x.Key,
					contentType: x.ContentType,
					volume: x.Volume,
					camCoordinates: x.CamCoordinates,
					panCoordinates: x.PanCoordinates
				))
			],
			ct: ct
		).ConfigureAwait(false);

		foreach (Cad cad in cads)
		{
			await cache.UpdateAsync(cad.Id, cad).ConfigureAwait(false);
		}

		return cads.ToDictionary(
			x => result.Items.First(x => x.Key == x.Key).Id,
			x => x.Id
		);
	}
}
