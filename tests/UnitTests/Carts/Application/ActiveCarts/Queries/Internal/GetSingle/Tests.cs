using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly GetActiveCartItemHandler handler;
	private readonly GetActiveCartItemQuery request = new(ValidBuyerId, ValidProductId);

	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, false, ct))
			.ReturnsAsync(CreateItem());
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleAsync(ValidBuyerId, ValidProductId, false, ct),
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
		await Assert.That(result.ProductId).IsEqualTo(ValidProductId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCartItemNotFound()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(() => handler.Handle(request with { ProductId = new() }, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(() => handler.Handle(request with { ProductId = new() }, ct));
	}
}
