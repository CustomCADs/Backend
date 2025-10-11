using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;

public sealed class GetCadPresignedUrlGetHandler(
	ICadReads reads,
	ICadStorageService storage,
	BaseCachingService<CadId, Cad> cache,
	IEnumerable<IFileDownloadPolicy<CadId>> policies
) : IQueryHandler<GetCadPresignedUrlGetQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetCadPresignedUrlGetQuery req, CancellationToken ct = default)
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

		return new(
			ContentType: cad.ContentType,
			PresignedUrl: await storage.GetPresignedGetUrlAsync(cad.Key, cad.ContentType).ConfigureAwait(false)
		);
	}
}
