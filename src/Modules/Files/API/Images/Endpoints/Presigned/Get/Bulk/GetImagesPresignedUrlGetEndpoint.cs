using CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Get.Bulk;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Files.API.Images.Endpoints.Presigned.Get.Bulk;

public class GetImagesPresignedUrlGetEndpoint(IRequestSender sender)
	: Endpoint<ImagesPresignedUrlGetRequest, DownloadFileResponse[]>
{
	public override void Configure()
	{
		Post("bulk-download");
		Group<PresignedGroup>();
		Description(x => x
			.WithSummary("Bulk Download")
			.WithDescription("Get URLs to download your Images")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ImagesPresignedUrlGetRequest req, CancellationToken ct)
	{
		DownloadFileResponse[] response = await sender.SendQueryAsync(
			query: new GetImagesPresignedUrlGetQuery(
				Ids: [.. req.Ids.Select(ImageId.New)],
				RelationType: req.RelationType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
