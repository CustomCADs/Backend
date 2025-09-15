using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.Repositories.ActiveCarts;

public sealed class Reads(CartsContext context) : IActiveCartReads
{
	public async Task<ActiveCartItem[]> AllAsync(AccountId buyerId, bool track = true, CancellationToken ct = default)
		=> await context.ActiveCartItems
			.WithTracking(track)
			.Where(x => x.BuyerId == buyerId)
			.OrderByDescending(x => x.AddedAt)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<ActiveCartItem[]> AllAsync(ProductId productId, bool track = true, CancellationToken ct = default)
		=> await context.ActiveCartItems
			.WithTracking(track)
			.Where(x => x.ProductId == productId)
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<ActiveCartItem?> SingleAsync(AccountId buyerId, ProductId productId, bool track = true, CancellationToken ct = default)
		=> await context.ActiveCartItems
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.BuyerId == buyerId && x.ProductId == productId, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsAsync(AccountId buyerId, CancellationToken ct = default)
		=> await context.ActiveCartItems
			.WithTracking(false)
			.AnyAsync(x => x.BuyerId == buyerId, ct)
			.ConfigureAwait(false);

	public async Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default)
		=> await context.ActiveCartItems
			.WithTracking(false)
			.Where(x => x.BuyerId == buyerId)
			.CountAsync(ct)
			.ConfigureAwait(false);

	public async Task<AccountId[]> AccountsWithAsync(ProductId productId, CancellationToken ct = default)
		=> await context.ActiveCartItems
			.WithTracking(false)
			.Where(x => x.ProductId == productId)
			.Select(x => x.BuyerId)
			.Distinct()
			.ToArrayAsync(ct)
			.ConfigureAwait(false);
}
