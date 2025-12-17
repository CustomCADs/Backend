using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Get.All;

public sealed class GetPurchasedCartsEndpoint(IRequestSender sender)
	: Endpoint<GetPurchasedCartsRequest, Result<GetPurchasedCartsResponse>, GetPurchasedCartsMapper>
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
				CallerId: User.AccountId,
				PaymentStatus: req.PaymentStatus,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
