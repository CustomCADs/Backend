using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.DecreaseQuantity;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	[Test]
	[Arguments(MaxValidQuantity)]
	[Arguments(MinValidQuantity)]
	public void Decrease_ShouldNotThrowException_WhenValid(int amount)
	{
		CreateItemWithDelivery()
			.IncreaseQuantity(amount)
			.DecreaseQuantity(amount);
	}

	[Test]
	[Arguments(MaxValidQuantity)]
	[Arguments(MinValidQuantity)]
	public void Decrease_ShouldThrowException_WhenInvalidAmount(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(() => CreateItemWithDelivery()
				.IncreaseQuantity(amount - 1)
				.DecreaseQuantity(amount));
	}

	[Test]
	[Arguments(MaxValidQuantity)]
	[Arguments(MinValidQuantity)]
	public void Decrease_ShouldThrowException_WhenNotForDelivery(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(() => CreateItem()
				.IncreaseQuantity(amount)
				.DecreaseQuantity(amount));
	}
}