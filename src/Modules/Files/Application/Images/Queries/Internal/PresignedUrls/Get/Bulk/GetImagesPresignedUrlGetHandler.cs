using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Get.Bulk;

public sealed class GetImagesPresignedUrlGetHandler(
	IImageReads reads,
	IImageStorageService storage,
	BaseCachingService<ImageId, Image> cache,
	IEnumerable<IFileDownloadPolicy<ImageId>> policies
) : IQueryHandler<GetImagesPresignedUrlGetQuery, DownloadFileResponse[]>
{
	public async Task<DownloadFileResponse[]> Handle(GetImagesPresignedUrlGetQuery req, CancellationToken ct)
	{
		List<DownloadFileResponse> response = [];
		foreach (ImageId id in req.Ids)
		{
			Image image = await cache.GetOrCreateAsync(
				id: id,
				factory: async () => await reads.SingleByIdAsync(id, track: false, ct).ConfigureAwait(false)
					?? throw CustomNotFoundException<Image>.ById(id)
			).ConfigureAwait(false);

			await policies.EvaluateAsync(
				context: new(image.Id, image.OwnerId, req.CallerId),
				req.RelationType
			).ConfigureAwait(false);

			response.Add(new(
				PresignedUrl: await storage.GetPresignedGetUrlAsync(image.Key).ConfigureAwait(false),
				ContentType: image.ContentType
			));
		}
		return [.. response];
	}
}
