using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetTracks;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetTrack;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetShipmentTracksHandler handler;
	private readonly GetShipmentTracksQuery request = new(ValidId);

	private readonly Mock<IShipmentReads> reads = new();
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly ShipmentTrackDto[] Statuses = CreateShipmentTracksDtos();

	public Tests()
	{
		handler = new(reads.Object, delivery.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment().Activate(ValidReferenceId));

		delivery.Setup(x => x.TrackAsync(ValidReferenceId, ct)).ReturnsAsync(Statuses);
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
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		delivery.Verify(
			x => x.TrackAsync(ValidReferenceId, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		Dictionary<DateTimeOffset, GetShipmentTracksDto> tracks = await handler.Handle(request, ct);

		// Assert
		await Assert.That(Statuses.ToDictionary(x => x.DateTime, x => new GetShipmentTracksDto(x.Message, x.Place))).IsEquivalentTo(tracks);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenShipmentStatusInvalid()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment());

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Shipment>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenShipmentNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Shipment);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Shipment>>(() => handler.Handle(request, ct));
	}

	private static ShipmentTrackDto[] CreateShipmentTracksDtos(int count = 4, string message = "Message")
		=> [..
			Enumerable.Range(1, count).Select(i => new ShipmentTrackDto(
				DateTime: DateTimeOffset.UtcNow.AddSeconds(i),
				Place: null,
				IsDelivered: false,
				Message: message + i
			))
		];
}
