using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.API.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Put.PresignedUrl.Cad;

public sealed class GetProductPutCadPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductPutCadPresignedUrlRequest, string>
{
	public override void Configure()
	{
		Post("presignedUrls/replace/cad");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Change Cad")
			.WithDescription("Change your Product's Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetProductPutCadPresignedUrlRequest req, CancellationToken ct)
	{
		string url = await sender.SendQueryAsync(
			query: new CreatorGetProductCadPresignedUrlPutQuery(
				Id: ProductId.New(req.Id),
				NewCad: req.File,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(url).ConfigureAwait(false);
	}
}
