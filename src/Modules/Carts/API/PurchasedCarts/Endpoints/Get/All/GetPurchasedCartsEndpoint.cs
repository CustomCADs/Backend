using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Get.All;

public sealed class GetPurchasedCartsEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartsRequest, Result<GetPurchasedCartsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<PurchasedCartsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all your Carts with Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetPurchasedCartsRequest req, CancellationToken ct)
	{
		Result<GetAllPurchasedCartsDto> result = await sender.SendQueryAsync(
			query: new GetAllPurchasedCartsQuery(
				CallerId: User.GetAccountId(),
				PaymentStatus: req.PaymentStatus,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(
			response: result.ToNewResult(x => x.ToResponse())
		).ConfigureAwait(false);
	}
}
