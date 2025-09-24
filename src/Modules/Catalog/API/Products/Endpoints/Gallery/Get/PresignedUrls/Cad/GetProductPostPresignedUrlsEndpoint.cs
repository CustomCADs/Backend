using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.PresignedUrls.Cad;

public sealed class GetProductGetPresignedUrlsEndpoint(IRequestSender sender)
	: Endpoint<GetProductGetPresignedUrlsRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download/cad");
		Group<GalleryGroup>();
		Description(x => x
			.WithSummary("Download Cad")
			.WithDescription("Download the Cad for a Product")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductGetPresignedUrlsRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			query: new GalleryGetProductCadPresignedUrlGetQuery(
				Id: ProductId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
