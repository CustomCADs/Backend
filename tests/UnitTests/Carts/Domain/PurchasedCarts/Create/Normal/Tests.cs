using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Create.Normal;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		PurchasedCart.Create(ValidBuyerId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WithoutItems()
	{
		var cart = PurchasedCart.Create(ValidBuyerId);

		using (Assert.Multiple())
		{
			await Assert.That(cart.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1)).IsTrue();
			await Assert.That(cart.Items).IsEmpty();
		}
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WithItems()
	{
		var items = CreateItems(noDeliveryCount: 1, forDeliveryCount: 1);
		var cads = items.ToDictionary(x => x.ProductId, x => CadId.New());

		var cart = CreateCartWithItems(
			buyerId: ValidBuyerId,
			items: items,
			prices: items.ToDictionary(x => x.ProductId, x => CartItemsData.MinValidPrice),
			productCads: cads,
			itemCads: cads.ToDictionary(x => x.Value, x => CadId.New())
		);

		using (Assert.Multiple())
		{
			await Assert.That(cart.TotalCost).IsEqualTo(cart.Items.Sum(x => x.Cost));
			await Assert.That(cart.Items.Count).IsEqualTo(items.Length);
		}
	}
}
