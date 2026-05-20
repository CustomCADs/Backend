using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly GetActiveCartItemHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, false, ct))
			.ReturnsAsync(CreateItem());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetActiveCartItemQuery query = new(ValidBuyerId, ValidProductId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(
			x => x.SingleAsync(ValidBuyerId, ValidProductId, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetActiveCartItemQuery query = new(ValidBuyerId, ValidProductId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidProductId, result.ProductId);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartItemNotFound()
	{
		// Arrange
		GetActiveCartItemQuery query = new(ValidBuyerId, ProductId.New());

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		GetActiveCartItemQuery query = new(ValidBuyerId, ProductId.New());

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
