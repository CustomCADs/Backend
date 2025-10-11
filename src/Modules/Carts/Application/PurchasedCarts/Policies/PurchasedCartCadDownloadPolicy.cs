using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Carts.Application.PurchasedCarts.Policies;

public class PurchasedCartCadDownloadPolicy(IPurchasedCartReads reads) : IFileDownloadPolicy<CadId>
{
	public FileContextType Type => FileContextType.PurchasedCart;

	public async Task EnsureDownloadGrantedAsync(IFileDownloadPolicy<CadId>.FileContext context)
	{
		PurchasedCart cart = await reads.SingleByCadIdAsync(context.FileId, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ByProp(nameof(PurchasedCartItem.CadId), context.FileId);

		if (cart.BuyerId != context.CallerId || context.OwnerId != context.CallerId)
		{
			throw CustomAuthorizationException<PurchasedCart>.ById(context.FileId, "Cad");
		}

		if (cart.PaymentStatus is not PaymentStatus.Completed)
		{
			throw CustomException.NotPaid<PurchasedCart>();
		}
	}
}
