using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.Count;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.Count;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly CountActiveCartItemsHandler handler;
	private readonly CountActiveCartItemsQuery request = new(ValidBuyerId);

	private readonly Mock<IActiveCartReads> reads = new();

	private const int Count = 5;

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountAsync(ValidBuyerId, ct))
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
			x => x.CountAsync(ValidBuyerId, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		int count = await handler.Handle(request, ct);

		// Assert
		await Assert.That(count).IsEqualTo(Count);
	}
}
