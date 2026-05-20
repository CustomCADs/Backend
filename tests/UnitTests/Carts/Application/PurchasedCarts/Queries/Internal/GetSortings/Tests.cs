using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly GetPurchasedCartSortingsHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetPurchasedCartSortingsQuery query = new();

		// Act
		PurchasedCartSortingType[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<PurchasedCartSortingType>());
	}
}
