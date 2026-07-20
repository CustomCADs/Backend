using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Shipments.Commands.Shared.Create;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly CreateShipmentHandler handler;
	private readonly CreateShipmentCommand request = new(
		Service: ValidService,
		Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
		Address: new(ValidCountry, ValidCity, ValidStreet),
		Contact: new(ValidPhone, ValidEmail),
		BuyerId: ValidBuyerId
	);

	private readonly Mock<IWrites<Shipment>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IDeliveryService> delivery = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, delivery.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Shipment>(x => x.BuyerId == ValidBuyerId),
			ct
		)).ReturnsAsync(CreateShipment());

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(true);

		delivery.Setup(x => x.ValidateAsync(ValidCountry, ValidCity, ValidStreet, ValidPhone, ct))
			.ReturnsAsync(true);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Shipment>(x => x.BuyerId == ValidBuyerId),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ShipmentId id = await handler.Handle(request, ct);

		// Assert
		await Assert.That(id).IsEqualTo(ValidId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenDeliveryDetailsInvalid()
	{
		// Arrange
		delivery.Setup(x => x.ValidateAsync(ValidCountry, ValidCity, ValidStreet, ValidPhone, ct))
			.ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomException>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenDesignerNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(() => handler.Handle(request, ct));
	}
}
