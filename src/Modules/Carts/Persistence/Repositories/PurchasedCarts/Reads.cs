using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.Repositories.PurchasedCarts;

public sealed class Reads(CartsContext context) : IPurchasedCartReads
{
	public async Task<Result<PurchasedCart>> AllAsync(PurchasedCartQuery query, bool track = true, CancellationToken ct = default)
	{
		IQueryable<PurchasedCart> queryable = context.PurchasedCarts
			.WithTracking(track)
			.Include(x => x.Items)
			.WithFilter(query.BuyerId, query.PaymentStatus);

		int count = await queryable.CountAsync(ct).ConfigureAwait(false);
		PurchasedCart[] carts = await queryable
			.WithSorting(query.Sorting)
			.WithPagination(query.Pagination)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

		return new(count, carts);
	}

	public async Task<PurchasedCart?> SingleByIdAsync(PurchasedCartId id, bool track = true, CancellationToken ct = default)
		=> await context.PurchasedCarts
			.WithTracking(track)
			.Include(x => x.Items)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default)
		=> await context.PurchasedCarts
			.WithTracking(false)
			.Where(x => x.BuyerId == buyerId)
			.CountAsync(ct)
			.ConfigureAwait(false);

	public async Task<Dictionary<PurchasedCartId, int>> CountItemsAsync(AccountId buyerId, CancellationToken ct = default)
		=> await context.PurchasedCarts
			.WithTracking(false)
			.Include(x => x.Items)
			.Where(x => x.BuyerId == buyerId)
			.ToDictionaryAsync(x => x.Id, x => x.Items.Count, ct)
			.ConfigureAwait(false);
}
