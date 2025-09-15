using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public sealed class GetCadPresignedUrlGetByIdHandler(
	ICadReads reads,
	ICadStorageService storage,
	BaseCachingService<CadId, Cad> cache
) : IQueryHandler<GetCadPresignedUrlGetByIdQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetCadPresignedUrlGetByIdQuery req, CancellationToken ct)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		return new(
			PresignedUrl: await storage.GetPresignedGetUrlAsync(cad.Key, cad.ContentType).ConfigureAwait(false),
			ContentType: cad.ContentType
		);
	}
}
