using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Shipments.Commands.Shared.Create;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create;

using static ShipmentsData;

public class CreateShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly CreateShipmentHandler handler;
	private readonly Mock<IWrites<Shipment>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IDeliveryService> delivery = new();

	public CreateShipmentHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, delivery.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Shipment>(x => x.BuyerId == ValidBuyerId),
			ct
		)).ReturnsAsync(CreateShipmentWithId());

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(true);

		delivery.Setup(x => x.ValidateAsync(ValidCountry, ValidCity, ValidStreet, ValidPhone, ct))
			.ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: ValidService,
			Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
			Address: new(ValidCountry, ValidCity, ValidStreet),
			Contact: new(ValidPhone, ValidEmail),
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Shipment>(x => x.BuyerId == ValidBuyerId),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: ValidService,
			Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
			Address: new(ValidCountry, ValidCity, ValidStreet),
			Contact: new(ValidPhone, ValidEmail),
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: ValidService,
			Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
			Address: new(ValidCountry, ValidCity, ValidStreet),
			Contact: new(ValidPhone, ValidEmail),
			BuyerId: ValidBuyerId
		);

		// Act
		ShipmentId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDeliveryDetailsInvalid()
	{
		// Arrange
		delivery.Setup(x => x.ValidateAsync(ValidCountry, ValidCity, ValidStreet, ValidPhone, ct))
			.ReturnsAsync(false);

		CreateShipmentCommand command = new(
			Service: ValidService,
			Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
			Address: new(ValidCountry, ValidCity, ValidStreet),
			Contact: new(ValidPhone, ValidEmail),
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDesignerNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(false);

		CreateShipmentCommand command = new(
			Service: ValidService,
			Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
			Address: new(ValidCountry, ValidCity, ValidStreet),
			Contact: new(ValidPhone, ValidEmail),
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
