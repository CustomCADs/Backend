using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Shared.Calculate.ToOffice;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared.Calculate.ToOffice;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly CalculateShipmentToOfficeHandler handler;
	private readonly CalculateShipmentToOfficeQuery request = new(Weights, ValidOfficeId);

	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly CalculationDto[] Calculations = [
		new(string.Empty, new(default, default, default, string.Empty), default, default)
	];
	private static readonly double[] Weights = [0, 1, 2, 3, 4, 5, 6];

	public Tests()
	{
		handler = new(delivery.Object);

		delivery.Setup(x => x.CalculateAsync(
			It.Is<CalculateRequest>(x =>
				x.Destination == Destination.Office
				&& !x.Address.HasValue
				&& x.OfficeId == ValidOfficeId
				&& x.Weights.Length == Weights.Length
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
					x.Destination == Destination.Office
					&& !x.Address.HasValue
					&& x.OfficeId == ValidOfficeId
					&& x.Weights.Length == Weights.Length
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
