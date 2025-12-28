using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public sealed record GetAllPurchasedCartsQuery(
	Pagination Pagination,
	AccountId? CallerId = null,
	PaymentStatus? PaymentStatus = null,
	Sorting<PurchasedCartSortingType>? Sorting = null
) : IQuery<Result<GetAllPurchasedCartsDto>>;
