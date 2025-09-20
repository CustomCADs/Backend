using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetTracks;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetTrack;

using static ShipmentsData;

public class GetShipmentTrackHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly GetShipmentTracksHandler handler;
	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly ShipmentTrackDto[] statuses = CreateShipmentTracksDtos();

	public GetShipmentTrackHandlerUnitTests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment().Activate(ValidReferenceId));

		delivery.Setup(x => x.TrackAsync(ValidReferenceId, ct)).ReturnsAsync(statuses);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetShipmentTracksQuery query = new(ValidId);

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
		GetShipmentTracksQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.TrackAsync(ValidReferenceId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetShipmentTracksQuery query = new(ValidId);

		// Act
		Dictionary<DateTimeOffset, GetShipmentTracksDto> tracks = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(tracks, statuses.ToDictionary(x => x.DateTime, x => new GetShipmentTracksDto(x.Message, x.Place)));
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenShipmentStatusInvalid()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment());
		GetShipmentTracksQuery query = new(ValidId);

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
		GetShipmentTracksQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
