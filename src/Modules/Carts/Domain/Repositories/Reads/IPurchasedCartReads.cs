using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public interface IPurchasedCartReads
{
	Task<Result<PurchasedCart>> AllAsync(PurchasedCartQuery query, bool track = true, CancellationToken ct = default);
	Task<PurchasedCart?> SingleByIdAsync(PurchasedCartId id, bool track = true, CancellationToken ct = default);
	Task<PurchasedCart?> SingleByCadIdAsync(CadId cadId, bool track = true, CancellationToken ct = default);
	Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default);
	Task<Dictionary<PurchasedCartId, int>> CountItemsAsync(AccountId buyerId, CancellationToken ct = default);
}
