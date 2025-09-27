using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.PresignedUrls.Image;

public sealed class GetProductGetPresignedImageUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductGetPresignedImageUrlRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download/image");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Download Image")
			.WithDescription("Download an Product's Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductGetPresignedImageUrlRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			query: new CreatorGetProductImagePresignedUrlGetQuery(
				Id: ProductId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
