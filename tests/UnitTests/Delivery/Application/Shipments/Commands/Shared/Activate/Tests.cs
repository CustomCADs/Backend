using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Application.Shipments.Commands.Shared.Activate;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Activate;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly ActivateShipmentHandler handler;
	private readonly ActivateShipmentCommand request = new(ValidId);

	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private readonly Shipment shipment = CreateShipment();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(shipment);

		delivery.Setup(x => x.ShipAsync(
			It.Is<ShipRequestDto>(
				x => x.Country == shipment.Address.Country
				&& x.City == shipment.Address.City
				&& x.Street == shipment.Address.Street
				&& x.Phone == shipment.Contact.Phone
				&& x.Email == shipment.Contact.Email
				&& x.Name == shipment.Info.Recipient
				&& x.Service == shipment.Reference.Service
				&& x.Contents.Contains(shipment.Info.Count.ToString())
				&& x.TotalWeight == shipment.Info.Weight
			),
			ct
		)).ReturnsAsync(new ShipmentDto(ValidReferenceId, [], 5, default, default));
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
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

	[Test]
	public async Task Handle_ShouldInitiateDelivery()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		delivery.Verify(
			x => x.ShipAsync(
				It.Is<ShipRequestDto>(
					x => x.Country == shipment.Address.Country
					&& x.City == shipment.Address.City
					&& x.Street == shipment.Address.Street
					&& x.Phone == shipment.Contact.Phone
					&& x.Email == shipment.Contact.Email
					&& x.Name == shipment.Info.Recipient
					&& x.Service == shipment.Reference.Service
					&& x.Contents.Contains(shipment.Info.Count.ToString())
					&& x.TotalWeight == shipment.Info.Weight
				),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldActivateShipment()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		await Assert.That(shipment.Status).IsEqualTo(ShipmentStatus.Active);
		await Assert.That(shipment.Reference.Id).IsEqualTo(ValidReferenceId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Shipment);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(() => handler.Handle(request, ct));
	}
}
