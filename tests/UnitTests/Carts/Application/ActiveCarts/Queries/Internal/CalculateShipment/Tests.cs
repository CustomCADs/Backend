using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly CalculateActiveCartShipmentHandler handler;
	private readonly CalculateActiveCartShipmentQuery request = new(ValidBuyerId, Address);

	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly AddressDto Address = new("Bulgaria", "Burgas", "Slivnitsa");
	private static readonly CalculateShipmentDto[] Calculations = [
		new(default, string.Empty, string.Empty, default, default)
	];

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync([
				CreateItem(ValidBuyerId, ValidProductId),
				CreateItemWithDelivery(ValidBuyerId, ValidProductId),
			]);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<BatchGetCustomizationWeightByIdQuery>(),
			ct
		)).ReturnsAsync([]);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<CalculateShipmentQuery>(),
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
			x => x.AllAsync(ValidBuyerId, false, ct),
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
				It.IsAny<BatchGetCustomizationWeightByIdQuery>(),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<CalculateShipmentQuery>(x => x.Address == Address),
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
}
