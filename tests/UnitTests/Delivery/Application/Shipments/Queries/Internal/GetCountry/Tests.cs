using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetCountry;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetCountry;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetCountryHandler handler;
	private readonly GetCountryQuery request = new("Bulgaria");

	private readonly Mock<IDeliveryService> delivery = new();

	public Tests()
	{
		handler = new(delivery.Object);

		delivery.Setup(x => x.GetCountryAsync(request.Country)).ReturnsAsync(ValidCountryId);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		int countryId = await handler.Handle(request, ct);

		// Assert
		await Assert.That(countryId).IsEquivalentTo(ValidCountryId);
	}
}
