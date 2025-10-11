using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;

public sealed class GetCadPresignedUrlPostHandler(
	ICadStorageService storage,
	IEnumerable<IFileUploadPolicy<CadId>> policies
) : IQueryHandler<GetCadPresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetCadPresignedUrlPostQuery req, CancellationToken ct = default)
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
