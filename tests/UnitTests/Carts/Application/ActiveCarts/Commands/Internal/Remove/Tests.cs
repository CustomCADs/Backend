using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Remove;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Remove;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly RemoveActiveCartItemHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IWrites<ActiveCartItem>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly ActiveCartItem item = CreateItem(productId: ValidProductId);

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
			.ReturnsAsync(item);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		RemoveActiveCartItemCommand command = new(
			CallerId: ValidBuyerId,
			ProductId: ValidProductId
		);

		// Act
		await handler.Handle(command, ct);

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
		RemoveActiveCartItemCommand command = new(
			CallerId: ValidBuyerId,
			ProductId: ValidProductId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
			.ReturnsAsync(null as ActiveCartItem);

		RemoveActiveCartItemCommand command = new(
			CallerId: ValidBuyerId,
			ProductId: ValidProductId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			() => handler.Handle(command, ct)
		);
	}
}
