using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly CountPurchasedCartsHandler handler;
	private readonly CountPurchasedCartsQuery request = new(ValidBuyerId);

	private readonly Mock<IPurchasedCartReads> reads = new();

	private const int Count = 4;

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountAsync(ValidBuyerId, ct))
			.ReturnsAsync(Count);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.CountAsync(ValidBuyerId, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		int count = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Count, count);
	}
}
