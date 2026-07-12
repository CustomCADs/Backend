using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly CalculateCustomShipmentHandler handler;
	private readonly CalculateCustomShipmentQuery request = new(ValidId, Quantity, Address, ValidCustomizationId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const int Quantity = 4;
	private const double Weight = 2.7;

	private static readonly AddressDto Address = new("Bulgaria", "Burgas", "Slivnitsa");
	private static readonly CalculateShipmentDto[] Calculations = [
		new(default, string.Empty, string.Empty, default, default)
	];

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateCustom(forDelivery: true));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationWeightByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(Weight);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<CalculateShipmentQuery>(x => x.Address == Address && x.Weights.Sum() == Quantity * Weight),
			ct
		)).ReturnsAsync(Calculations);
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
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCustomizationWeightByIdQuery>(x => x.Id == ValidCustomizationId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<CalculateShipmentQuery>(x => x.Address == Address && x.Weights.Sum() == Quantity * Weight),
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
		CalculateShipmentDto[] result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEquivalentTo(Calculations);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenNoItemsForDelivery()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateCustom(forDelivery: false));

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			() => handler.Handle(request, ct)
		);
	}
}
