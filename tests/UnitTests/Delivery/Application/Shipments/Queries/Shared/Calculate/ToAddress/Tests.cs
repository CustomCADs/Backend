using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Shared.Calculate.ToAddress;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared.Calculate.ToAddress;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly CalculateShipmentToAddressHandler handler;
	private readonly CalculateShipmentToAddressQuery request = new(TotalWeight, ValidCountry, ValidCity);

	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly CalculationDto[] Calculations = [
		new(string.Empty, new(default, default, default, string.Empty), default, default)
	];
	private const double TotalWeight = 17;

	public Tests()
	{
		handler = new(delivery.Object);

		delivery.Setup(x => x.CalculateAsync(
			It.Is<CalculateRequest>(x =>
				x.Destination == DeliveryDestination.Address
				&& x.Address.HasValue
				&& x.Address.Value.Country == ValidCountry
				&& x.Address.Value.Site == ValidCity
				&& x.TotalWeight == TotalWeight
			),
			ct
		)).ReturnsAsync(Calculations);
	}

	[Test]
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		delivery.Verify(
			x => x.CalculateAsync(
				It.Is<CalculateRequest>(x =>
					x.Destination == DeliveryDestination.Address
					&& x.Address.HasValue
					&& x.Address.Value.Country == ValidCountry
					&& x.Address.Value.Site == ValidCity
					&& x.TotalWeight == TotalWeight
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
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result.Length).IsEqualTo(Calculations.Length);
	}
}
