using CustomCADs.Modules.Customs.Application.Customs.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Events.Application.DeliveryRequested;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly CustomDeliveryRequestedHandler handler;
	private readonly CustomDeliveryRequestedApplicationEvent request = new(
		CustomId: ValidId,
		ShipmentService: ShipmentService,
		Weight: Weight,
		Count: Count,
		Address: Address,
		Contact: Contact
	);


	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string ShipmentService = "shipment-service";
	private const double Weight = 5.2;
	private const int Count = 3;
	private static readonly AddressDto Address = new("Bulgaria", "Burgas", "Slivnitsa");
	private static readonly ContactDto Contact = new("0123456789", null);
	private readonly Custom custom = CreateCustom(forDelivery: true);

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(ValidCustomizationId);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == custom.BuyerId),
			ct
		)).ReturnsAsync("NinjataBG");

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateShipmentCommand>(x => x.BuyerId == custom.BuyerId),
			ct
		)).ReturnsAsync(ValidShipmentId);
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == custom.BuyerId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<CreateShipmentCommand>(x => x.BuyerId == custom.BuyerId),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		Assert.Equal(ValidShipmentId, custom.CompletedCustom?.ShipmentId);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			() => handler.HandleAsync(request)
		);
	}
}
