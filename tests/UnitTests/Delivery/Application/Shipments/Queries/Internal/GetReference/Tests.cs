using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetReference;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetReference;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetReferenceHandler handler;
	private readonly GetReferenceQuery request = new(ValidId, ValidBuyerId);

	private readonly Mock<IShipmentReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateShipment().Activate(ValidReferenceId).Deliver());
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
		GetReferenceDto reference = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(reference.Id).IsEquivalentTo(ValidReferenceId);
			await Assert.That(reference.Service).IsEquivalentTo(ValidService);
		}
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
