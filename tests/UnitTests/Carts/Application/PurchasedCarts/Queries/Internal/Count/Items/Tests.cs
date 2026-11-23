using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly CountPurchasedCartItemsHandler handler;
	private readonly CountPurchasedCartItemsQuery request = new(ValidBuyerId);

	private readonly Mock<IPurchasedCartReads> reads = new();

	private static readonly Dictionary<PurchasedCartId, int> Count = new() { [ValidId] = 4 };

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountItemsAsync(ValidBuyerId, ct))
			.ReturnsAsync(Count);

		reads.Setup(x => x.CountItemsAsync(ValidBuyerId, ct))
			.ReturnsAsync(Count);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.CountItemsAsync(ValidBuyerId, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEquivalentTo(Count);
	}
}
