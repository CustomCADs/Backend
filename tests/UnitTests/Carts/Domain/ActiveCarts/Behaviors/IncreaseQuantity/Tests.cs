using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.IncreaseQuantity;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Test]
	[Arguments(MaxValidQuantity)]
	[Arguments(MinValidQuantity)]
	public void Increase_ShouldNotThrowException_WhenValid(int amount)
	{
		CreateItemWithDelivery().IncreaseQuantity(amount);
	}

	[Test]
	[Arguments(MaxInvalidQuantity)]
	[Arguments(MinInvalidQuantity - 1)]
	public void Increase_ShouldThrowException_WhenInvalidAmount(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(() => CreateItemWithDelivery().IncreaseQuantity(amount));
	}

	[Test]
	[Arguments(MaxValidQuantity)]
	[Arguments(MinValidQuantity)]
	public void Increase_ShouldThrowException_WhenNotForDelivery(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(() => CreateItem().IncreaseQuantity(amount));
	}
}