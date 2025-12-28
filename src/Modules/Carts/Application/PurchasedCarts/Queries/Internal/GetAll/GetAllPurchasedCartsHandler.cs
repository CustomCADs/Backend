using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public sealed class GetAllPurchasedCartsHandler(IPurchasedCartReads reads)
	: IQueryHandler<GetAllPurchasedCartsQuery, Result<GetAllPurchasedCartsDto>>
{
	public async Task<Result<GetAllPurchasedCartsDto>> Handle(GetAllPurchasedCartsQuery req, CancellationToken ct)
	{
		Result<PurchasedCart> result = await reads.AllAsync(
			query: new(
				BuyerId: req.CallerId,
				PaymentStatus: req.PaymentStatus,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGetAllDto());
	}
}
