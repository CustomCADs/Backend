using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToAddress;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToAddress;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly CalculateActiveCartShipmentToAddressHandler handler;
	private readonly CalculateActiveCartShipmentToAddressQuery request = new(ValidBuyerId, CountryId, SiteId);

	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const int CountryId = 100;
	private const int SiteId = 68134;
	private static readonly Dictionary<CustomizationId, double> Weights = new()
	{
		[CustomizationId.New()] = 4.3,
		[CustomizationId.New()] = 2.5,
		[CustomizationId.New()] = 3.7,
	};
	private static readonly ActiveCartItem[] Items = [..
		Weights.Keys.Select((x) => CreateItemWithDelivery(customizationId: x))
	];
	private static readonly double TotalWeight = Weights.Sum(x =>
		x.Value * Items.First(item => item.CustomizationId == x.Key).Quantity / 1000
	);

	private static readonly CalculateShipmentDto[] Calculations = [
		new(default, string.Empty, string.Empty, default, default)
	];

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync([
				CreateItem(),
				..Weights.Keys.Select((x) => CreateItemWithDelivery(customizationId: x)),
			]);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<BatchGetCustomizationWeightByIdQuery>(),
			ct
		)).ReturnsAsync(Weights);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<CalculateShipmentToAddressQuery>(x =>
				x.Weights.Sum() == TotalWeight
				&& x.CountryId == CountryId
				&& x.SiteId == SiteId
			),
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
				It.Is<CalculateShipmentToAddressQuery>(x =>
					x.Weights.Sum() == TotalWeight
					&& x.CountryId == CountryId
					&& x.SiteId == SiteId
				),
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
		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync([CreateItem()]);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			() => handler.Handle(request, ct)
		);
	}
}
