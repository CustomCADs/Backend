using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly GetPurchasedCartSortingsHandler handler = new();
	private readonly GetPurchasedCartSortingsQuery request = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		PurchasedCartSortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<PurchasedCartSortingType>());
	}
}
