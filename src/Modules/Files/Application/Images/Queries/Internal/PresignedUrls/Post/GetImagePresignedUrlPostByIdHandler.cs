using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Post;

public sealed class GetImagePresignedUrlPostHandler(
	IImageStorageService storage,
	IEnumerable<IFileUploadPolicy<ImageId>> policies
)
	: IQueryHandler<GetImagePresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetImagePresignedUrlPostQuery req, CancellationToken ct)
	{
		await policies.EvaluateAsync(
			context: new(req.CallerId),
			type: req.RelationType
		).ConfigureAwait(false);

		return await storage.GetPresignedPostUrlAsync(
			name: req.Name,
			file: req.File
		).ConfigureAwait(false);
	}
}
