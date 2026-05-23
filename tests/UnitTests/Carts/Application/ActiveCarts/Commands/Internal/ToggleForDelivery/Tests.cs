using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Commands;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly ToggleActiveCartItemForDeliveryHandler handler;
	private readonly ToggleActiveCartItemForDeliveryCommand request = new(
		CallerId: ValidBuyerId,
		ProductId: ProductId1,
		CustomizationId: null
	);
	private readonly ToggleActiveCartItemForDeliveryCommand requestForDelivery = new(
		CallerId: ValidBuyerId,
		ProductId: ProductId2,
		CustomizationId: ValidCustomizationId
	);

	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();


	private static readonly ProductId ProductId1 = ProductId.New();
	private static readonly ProductId ProductId2 = ProductId.New();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ProductId1, true, ct))
			.ReturnsAsync(CreateItemWithDelivery(ValidBuyerId, ProductId1));

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ProductId2, true, ct))
			.ReturnsAsync(CreateItem(ValidBuyerId, ProductId2));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleAsync(ValidBuyerId, ProductId1, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenTurningDeliveryOff()
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
	public async Task Handle_ShouldPersistToDatabase_WhenTurningDeliveryOn()
	{
		// Arrange

		// Act
		await handler.Handle(requestForDelivery, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenTurningDeliveryOff()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<DeleteCustomizationByIdCommand>(x => x.Id == ValidCustomizationId),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenTurningDeliveryOn()
	{
		// Arrange

		// Act
		await handler.Handle(requestForDelivery, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(ValidBuyerId, ProductId1, true, ct))
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
			() => handler.Handle(request with { ProductId = ValidProductId }, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			() => handler.Handle(requestForDelivery, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDeliveryMismatch()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			() => handler.Handle(requestForDelivery with { CustomizationId = null }, ct)
		);
	}
}
