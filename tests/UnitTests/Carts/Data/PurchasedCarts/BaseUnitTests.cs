using CustomCADs.Modules.Carts.Domain.ActiveCarts;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Data.PurchasedCarts;

using static TestData;
using static TestData.CartItemsData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static PurchasedCart CreateCart(AccountId? buyerId = null, PurchasedCartId? id = null)
		=> PurchasedCart.CreateWithId(id ?? ValidId, buyerId ?? ValidBuyerId);

	protected static PurchasedCart CreateCartWithItems(
		AccountId buyerId,
		ActiveCartItem[] items,
		Dictionary<ProductId, decimal> prices,
		Dictionary<ProductId, CadId> productCads,
		Dictionary<CadId, CadId> itemCads
	)
	{
		var purchasedCart = CreateCart(buyerId);

		purchasedCart.AddItems([.. items.Select(item => {
			decimal price = prices[item.ProductId];
			CadId productCadId = productCads[item.ProductId];
			CadId itemCadId = itemCads[productCadId];

			return new CartItemDto(price, itemCadId, item.ProductId, item.ForDelivery, item.CustomizationId, item.Quantity, item.AddedAt);
		})]);

		return purchasedCart;
	}

	protected static PurchasedCart CreateCartWithItems(PurchasedCartId? id = null, AccountId? buyerId = null, params PurchasedCartItem[] items)
	{
		var purchasedCart = CreateCart(buyerId, id);
		purchasedCart.AddItems([.. items.Select(i =>
			new CartItemDto(
				Price: i.Price,
				CadId: i.CadId,
				ProductId: i.ProductId,
				ForDelivery: i.ForDelivery,
				CustomizationId: i.CustomizationId,
				Quantity: i.Quantity,
				AddedAt: i.AddedAt
			)
		)]);

		return purchasedCart;
	}

	protected static ActiveCartItem[] CreateItems(int noDeliveryCount, int forDeliveryCount)
	{
		List<ActiveCartItem> items = [];

		for (int i = 0; i < noDeliveryCount; i++)
		{
			items.Add(ActiveCartItem.Create(ProductId.New(), ValidBuyerId));
			items.Add(ActiveCartItem.Create(ProductId.New(), ValidBuyerId));
		}
		for (int i = 0; i < forDeliveryCount; i++)
		{
			items.Add(ActiveCartItem.Create(ProductId.New(), ValidBuyerId, ValidCustomizationId));
			items.Add(ActiveCartItem.Create(ProductId.New(), ValidBuyerId, ValidCustomizationId));
		}

		return [.. items];
	}

	protected static PurchasedCartItem CreateItem(
		PurchasedCartId? cartId = null,
		ProductId? productId = null,
		CadId? cadId = null,
		CustomizationId? customizationId = null,
		decimal? price = null,
		int? quantity = null,
		bool? forDelivery = null
	) => PurchasedCartItem.Create(
			cartId: cartId ?? ValidId,
			productId: productId ?? ValidProductId,
			cadId: cadId ?? ValidCadId,
			customizationId: customizationId ?? ValidCustomizationId,
			price: price ?? MinValidPrice,
			quantity: quantity ?? MinValidQuantity,
			forDelivery: forDelivery ?? false,
				addedAt: DateTimeOffset.UtcNow
		);
}
