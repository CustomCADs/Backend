using CustomCADs.Modules.Files.Application.Cads.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;

public sealed class GetCadPresignedUrlPutHandler(
	ICadReads reads,
	ICadStorageService storage,
	BaseCachingService<CadId, Cad> cache,
	IEnumerable<IFileReplacePolicy<CadId>> policies
) : IQueryHandler<GetCadPresignedUrlPutQuery, string>
{
	public async Task<string> Handle(GetCadPresignedUrlPutQuery req, CancellationToken ct = default)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		await policies.EvaluateAsync(
			context: new(cad.Id, cad.OwnerId, req.CallerId),
			type: req.RelationType
		).ConfigureAwait(false);

		return await storage.GetPresignedPutUrlAsync(
			key: cad.Key,
			file: req.File
		).ConfigureAwait(false);
	}
}
