using CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Post;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Printing.API.Materials.Endpoints.Post.PresignedUrl;

public sealed class GetMaterialPostPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetMaterialPostPresignedUrlRequest, UploadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/upload");
		Group<MaterialsGroup>();
		Description(x => x
			.WithSummary("Upload Texture")
			.WithDescription("Upload your Material's Texture")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetMaterialPostPresignedUrlRequest req, CancellationToken ct)
	{
		UploadFileResponse response = await sender.SendQueryAsync(
			query: new GetMaterialTexturePresignedUrlPostQuery(
				MaterialName: req.MaterialName,
				Image: req.Image
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
