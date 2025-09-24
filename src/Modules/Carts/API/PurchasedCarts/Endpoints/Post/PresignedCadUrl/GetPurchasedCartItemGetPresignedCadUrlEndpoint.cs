using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed class GetPurchasedCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartItemGetPresignedCadUrlRequest, GetPurchasedCartItemCadPresignedUrlGetDto>
{
	public override void Configure()
	{
		Post("presignedUrls/download/response");
		Group<PurchasedCartsGroup>();
		Description(x => x
			.WithSummary("Download Cad")
			.WithDescription("Download your Cart Item's Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetPurchasedCartItemGetPresignedCadUrlRequest req, CancellationToken ct)
	{
		GetPurchasedCartItemCadPresignedUrlGetDto response = await sender.SendQueryAsync(
			query: new GetPurchasedCartItemCadPresignedUrlGetQuery(
				Id: PurchasedCartId.New(req.Id),
				ProductId: ProductId.New(req.ProductId),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
