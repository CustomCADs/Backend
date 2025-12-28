using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Get;

public sealed class GetImagePresignedUrlGetHandler(
	IImageReads reads,
	IImageStorageService storage,
	BaseCachingService<ImageId, Image> cache,
	IEnumerable<IFileDownloadPolicy<ImageId>> policies
) : IQueryHandler<GetImagePresignedUrlGetQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetImagePresignedUrlGetQuery req, CancellationToken ct)
	{
		Image image = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Image>.ById(req.Id)
		).ConfigureAwait(false);

		await policies.EvaluateAsync(
			context: new(image.Id, image.OwnerId, req.CallerId),
			req.RelationType
		).ConfigureAwait(false);

		return new(
			PresignedUrl: await storage.GetPresignedGetUrlAsync(image.Key).ConfigureAwait(false),
			ContentType: image.ContentType
		);
	}
}
