using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetSortings;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetSortings;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetShipmentSortingsHandler handler = new();
	private readonly GetShipmentSortingsQuery request = new();

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ShipmentSortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		await Assert.That(Enum.GetValues<ShipmentSortingType>()).IsEquivalentTo(sortings);
	}
}
