using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;

public sealed class GetPurchasedCartSortingsHandler : IQueryHandler<GetPurchasedCartSortingsQuery, PurchasedCartSortingType[]>
{
	public Task<PurchasedCartSortingType[]> Handle(GetPurchasedCartSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<PurchasedCartSortingType>()
		);
}
