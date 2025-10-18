using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Put;

public sealed class GetImagePresignedUrlPutHandler(
	IImageReads reads,
	IImageStorageService storage,
	BaseCachingService<ImageId, Image> cache,
	IEnumerable<IFileReplacePolicy<ImageId>> policies
) : IQueryHandler<GetImagePresignedUrlPutQuery, string>
{
	public async Task<string> Handle(GetImagePresignedUrlPutQuery req, CancellationToken ct)
	{
		Image image = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Image>.ById(req.Id)
		).ConfigureAwait(false);

		await policies.EvaluateAsync(
			context: new(image.Id, image.OwnerId, req.CallerId),
			type: req.RelationType
		).ConfigureAwait(false);

		return await storage.GetPresignedPutUrlAsync(
			key: image.Key,
			file: req.File
		).ConfigureAwait(false);
	}
}
