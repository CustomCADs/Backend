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
	private readonly GetShipmentWaybillQuery request = new(ValidId, HeadDesignerAccountId);

	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly byte[] Bytes = [1, 2, 3, 4, 5, 6];

	public Tests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment().Activate(ValidReferenceId));

		delivery.Setup(x => x.PrintAsync(ValidReferenceId, ct)).ReturnsAsync(Bytes);
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		delivery.Verify(
			x => x.PrintAsync(ValidReferenceId, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		byte[] result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(result, Bytes);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCallerNotHeadDesigner()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Shipment>>(
			// Act
			() => handler.Handle(request with { CallerId = ValidBuyerId }, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentStatusInvalid()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateShipment());

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Shipment>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Shipment);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
