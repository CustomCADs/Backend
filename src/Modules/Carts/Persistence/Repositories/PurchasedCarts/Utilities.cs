using CustomCADs.Modules.Carts.Domain.PurchasedCarts;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Carts.Persistence.Repositories.PurchasedCarts;

internal static class Utilities
{
	extension(IQueryable<PurchasedCart> query)
	{
		internal IQueryable<PurchasedCart> WithFilter(AccountId? buyerId = null, PaymentStatus? paymentStatus = null)
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

		internal IQueryable<PurchasedCart> WithSorting(Sorting<PurchasedCartSortingType>? sorting = null)
			=> sorting?.Type switch
			{
				PurchasedCartSortingType.PurchasedAt => query.ToSorted(sorting, x => x.PurchasedAt),
				PurchasedCartSortingType.Total => query.ToSorted(sorting, x => x.Items.Sum(x => x.Quantity * x.Price)),
				_ => query,
			};
	}
}
