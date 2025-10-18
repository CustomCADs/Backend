using CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Put;
using CustomCADs.Shared.API.Attributes;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Files.API.Images.Endpoints.Presigned.Put;

public class ImagePresignedUrlPutEndpoint(IRequestSender sender) : Endpoint<ImagePresignedUrlPutRequest, string>
{
	public override void Configure()
	{
		Post("replace");
		Group<PresignedGroup>();
		Description(x => x
			.WithSummary("Replace")
			.WithDescription("Get a URL to replace your Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ImagePresignedUrlPutRequest req, CancellationToken ct)
	{
		string url = await sender.SendQueryAsync(
			query: new GetImagePresignedUrlPutQuery(
				Id: ImageId.New(req.Id),
				File: req.File,
				RelationType: req.RelationType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(url).ConfigureAwait(false);
	}
}
