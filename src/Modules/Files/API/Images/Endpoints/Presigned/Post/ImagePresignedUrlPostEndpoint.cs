using CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Post;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Files.API.Images.Endpoints.Presigned.Post;

public class ImagePresignedUrlPostEndpoint(IRequestSender sender) : Endpoint<ImagePresignedUrlPostRequest, UploadFileResponse>
{
	public override void Configure()
	{
		Post("upload");
		Group<PresignedGroup>();
		Description(x => x
			.WithSummary("Upload")
			.WithDescription("Get a URL to upload your Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ImagePresignedUrlPostRequest req, CancellationToken ct)
	{
		UploadFileResponse response = await sender.SendQueryAsync(
			query: new GetImagePresignedUrlPostQuery(
				Name: req.Name,
				File: req.File,
				RelationType: req.RelationType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
