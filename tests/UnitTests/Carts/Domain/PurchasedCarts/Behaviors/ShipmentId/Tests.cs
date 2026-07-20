using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Behaviors.ShipmentId;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	[Test]
	public void SetShipmentId_ShouldNotThrowException_WhenIsDelivery()
	{
		var items = CreateItems(noDeliveryCount: 1, forDeliveryCount: 1);
		var cads = items.ToDictionary(x => x.ProductId, x => CadId.New());

		CreateCartWithItems(
			buyerId: ValidBuyerId,
			items: items,
			prices: items.ToDictionary(x => x.ProductId, x => CartItemsData.MinValidPrice),
			productCads: cads,
			itemCads: cads.ToDictionary(x => x.Value, x => CadId.New())
		).SetShipmentId(ValidShipmentId);
	}

	[Test]
	public void SetShipmentId_ShouldThrowException_WhenNotDelivery()
	{
		var items = CreateItems(noDeliveryCount: 2, forDeliveryCount: 0);
		var cads = items.ToDictionary(x => x.ProductId, x => CadId.New());

		Assert.Throws<CustomValidationException<PurchasedCart>>(() => CreateCartWithItems(
				buyerId: ValidBuyerId,
				items: items,
				prices: items.ToDictionary(x => x.ProductId, x => CartItemsData.MinValidPrice),
				productCads: cads,
				itemCads: cads.ToDictionary(x => x.Value, x => CadId.New())
			).SetShipmentId(ValidShipmentId));
	}
}