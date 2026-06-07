using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrease;

using static ActiveCartItemConstants;
using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly DecreaseActiveCartItemQuantityHandler handler;
	private readonly DecreaseActiveCartItemQuantityCommand request = new(
		CallerId: ValidBuyerId,
		ProductId: ValidProductId,
		Amount: MinValidQuantity
	);

	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly int oldQuantity;

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

		var item = CreateItemWithDelivery(productId: ValidProductId).IncreaseQuantity(QuantityMax - 1);
		oldQuantity = item.Quantity;

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
			.ReturnsAsync(item);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		int result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(oldQuantity - request.Amount, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
			.ReturnsAsync(null as ActiveCartItem);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenItemNotFound()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			() => handler.Handle(request with { ProductId = new() }, ct)
		);
	}
}
