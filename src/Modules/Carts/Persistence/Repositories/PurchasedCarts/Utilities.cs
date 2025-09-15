using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Carts.Persistence.Repositories.PurchasedCarts;

public static class Utilities
{
	public static IQueryable<PurchasedCart> WithFilter(this IQueryable<PurchasedCart> query, AccountId? buyerId = null, PaymentStatus? paymentStatus = null)
	{
		if (buyerId is not null)
		{
			query = query.Where(x => x.BuyerId == buyerId);
		}
		if (paymentStatus is not null)
		{
			query = query.Where(x => x.PaymentStatus == paymentStatus);
		}

		return query;
	}

	public static IQueryable<PurchasedCart> WithSorting(this IQueryable<PurchasedCart> query, Sorting<PurchasedCartSortingType>? sorting = null)
		=> sorting?.Type switch
		{
			PurchasedCartSortingType.PurchasedAt => query.ToSorted(sorting, x => x.PurchasedAt),
			PurchasedCartSortingType.Total => query.ToSorted(sorting, x => x.Items.Sum(x => x.Quantity * x.Price)),
			_ => query,
		};
}
