using CustomCADs.Modules.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Data.ActiveCarts;

using static TestData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static ActiveCartItem CreateItem(
		AccountId? buyerId = null,
		ProductId? productId = null
	) => ActiveCartItem.Create(
			buyerId: buyerId ?? ValidBuyerId,
			productId: productId ?? ValidProductId
		);

	protected static ActiveCartItem CreateItemWithDelivery(
		AccountId? buyerId = null,
		ProductId? productId = null,
		CustomizationId? customizationId = null
	) => ActiveCartItem.Create(
			buyerId: buyerId ?? ValidBuyerId,
			productId: productId ?? ValidProductId,
			customizationId: customizationId ?? ValidCustomizationId
		);
}
