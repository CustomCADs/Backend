using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.API.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Put.PresignedUrl.Image;

public sealed class GetProductPutPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductPutPresignedUrlRequest, string>
{
	public override void Configure()
	{
		Post("presignedUrls/replace/image");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Change Image")
			.WithDescription("Change your Product's Image")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductPutPresignedUrlRequest req, CancellationToken ct)
	{
		string url = await sender.SendQueryAsync(
			query: new CreatorGetProductImagePresignedUrlPutQuery(
				Id: ProductId.New(req.Id),
				NewImage: req.File,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);
		await Send.OkAsync(url).ConfigureAwait(false);
	}
}
