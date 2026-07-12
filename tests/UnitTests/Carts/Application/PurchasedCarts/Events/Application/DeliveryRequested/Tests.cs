using CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;

using static Data.PurchasedCarts.TestData;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly ActiveCartDeliveryRequestedHandler handler;
	private readonly ActiveCartDeliveryRequestedApplicationEvent request = new(
		PurchasedCartId: ValidId,
		ShipmentService: string.Empty,
		Weight: default,
		Count: default,
		Address: new(string.Empty, string.Empty, string.Empty),
		Contact: new(default, default)
	);


	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly PurchasedCart cart = CreateCartWithItems(
		items: [CreateItem(forDelivery: true)]
	);

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(cart);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == cart.BuyerId),
			ct
		)).ReturnsAsync("NinjataBG");

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateShipmentCommand>(x => x.BuyerId == cart.BuyerId),
			ct
		)).ReturnsAsync(ValidShipmentId);
	}

	[Test]
	public async Task Handle_ShouldCalculateIdCorrectly()
	{
		// Arrange

		// Act

		// Assert
		await Assert.That(request.Id).IsEqualTo(request.PurchasedCartId.Value);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == cart.BuyerId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<CreateShipmentCommand>(x => x.BuyerId == cart.BuyerId),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as PurchasedCart);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(() => handler.HandleAsync(request));
	}
}
