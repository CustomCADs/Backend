using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Get.Sortings;

public sealed class GetPurchasedCartSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<PurchasedCartSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<PurchasedCartsGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Purchased Cart Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		PurchasedCartSortingType[] result = await sender.SendQueryAsync(
			query: new GetPurchasedCartSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
