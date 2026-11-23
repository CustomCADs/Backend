using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetShipment;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetSingle;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetShipmentHandler handler;
	private readonly GetShipmentQuery request = new(ValidId, ValidBuyerId);

	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private const double Fee = 15.3;

	public Tests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment());

		delivery.Setup(x => x.CalculateAsync(
			It.Is<CalculateRequest>(x => x.TotalWeight == MinValidWeight),
			ct
		)).ReturnsAsync([
			new(
				Service: ValidService,
				Price: new(
					Amount: Fee,
					Vat: default,
					Total: default,
					Currency: string.Empty
				),
				PickupDate: default,
				DeliveryDeadline: default
			)
		]);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		GetShipmentDto shipment = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(shipment.Id).IsEquivalentTo(ValidId);
			await Assert.That(shipment.Status).IsEquivalentTo(ShipmentStatus.Awaiting);
			await Assert.That(shipment.Info.Count).IsEquivalentTo(MinValidCount);
			await Assert.That(shipment.Info.Weight).IsEquivalentTo(MinValidWeight);
			await Assert.That(shipment.Info.Recipient).IsEquivalentTo(ValidRecipient);
			await Assert.That(shipment.Fee).IsEquivalentTo(Fee);
		}
	}

	[Test]
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		delivery.Verify(
			x => x.CalculateAsync(It.Is<CalculateRequest>(x => x.TotalWeight == MinValidWeight)),
			Times.Once()
		);
	}


	[Test]
	public async Task Handle_ShouldThrowException_WhenUnauthorized()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment(buyerId: AccountId.New()));

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Shipment>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Shipment);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(() => handler.Handle(request, ct));
	}
}
