using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Get.Single;

public sealed class GetPurchasedCartEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartRequest, GetPurchasedCartResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<PurchasedCartsGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See your Cart in detail")
		);
	}

	public override async Task HandleAsync(GetPurchasedCartRequest req, CancellationToken ct)
	{
		GetPurchasedCartByIdDto cart = await sender.SendQueryAsync(
			query: new GetPurchasedCartByIdQuery(
				Id: PurchasedCartId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		GetPurchasedCartResponse response = cart.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
