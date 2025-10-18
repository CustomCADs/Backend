using CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Get;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Files.API.Images.Endpoints.Presigned.Get;

public class ImagePresignedUrlGetEndpoint(IRequestSender sender) : Endpoint<ImagePresignedUrlGetRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("download");
		Group<PresignedGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Download")
			.WithDescription("Get a URL to download your Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ImagePresignedUrlGetRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			query: new GetImagePresignedUrlGetQuery(
				Id: ImageId.New(req.Id),
				RelationType: req.RelationType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
