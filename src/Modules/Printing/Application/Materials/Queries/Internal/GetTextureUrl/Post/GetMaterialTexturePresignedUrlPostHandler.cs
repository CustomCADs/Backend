using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed class GetMaterialTexturePresignedUrlPostHandler(IRequestSender sender)
	: IQueryHandler<GetMaterialTexturePresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetMaterialTexturePresignedUrlPostQuery req, CancellationToken ct)
		=> await sender.SendQueryAsync(
				query: new GetImagePresignedUrlPostByIdQuery(
					Name: req.MaterialName,
					File: req.Image
				),
				ct: ct
			).ConfigureAwait(false);
}
