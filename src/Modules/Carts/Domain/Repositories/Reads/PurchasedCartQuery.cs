using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Carts.Domain.Repositories.Reads;

public record PurchasedCartQuery(
	Pagination Pagination,
	AccountId? BuyerId = null,
	PaymentStatus? PaymentStatus = null,
	DateTimeOffset? Start = null,
	DateTimeOffset? End = null,
	bool? HasDelivery = null,
	Sorting<PurchasedCartSortingType>? Sorting = null
);
