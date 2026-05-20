using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetWaybill;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

using static DomainConstants.Users;
using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetShipmentWaybillHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly byte[] bytes = [1, 2, 3, 4, 5, 6];

	public Tests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment().Activate(ValidReferenceId));

		delivery.Setup(x => x.PrintAsync(ValidReferenceId, ct)).ReturnsAsync(bytes);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, HeadDesignerAccountId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, HeadDesignerAccountId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.PrintAsync(ValidReferenceId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, HeadDesignerAccountId);

		// Act
		byte[] result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(result, bytes);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCallerNotHeadDesigner()
	{
		// Arrange
		GetShipmentWaybillQuery query = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentStatusInvalid()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateShipment());
		GetShipmentWaybillQuery query = new(ValidId, HeadDesignerAccountId);

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Shipment);
		GetShipmentWaybillQuery query = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
