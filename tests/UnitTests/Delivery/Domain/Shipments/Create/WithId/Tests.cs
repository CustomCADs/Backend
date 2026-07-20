using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid()
	{
		CreateShipment();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WhenShipmentIsValid()
	{
		var shipment = CreateShipment();

		using (Assert.Multiple())
		{
			await Assert.That(shipment.Id).IsEqualTo(ValidId);
			await Assert.That(shipment.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(shipment.Address).IsEqualTo(new(ValidCountry, ValidCity, ValidStreet));
			await Assert.That(shipment.Info).IsEqualTo(new(MinValidCount, MinValidWeight, ValidRecipient));
			await Assert.That(shipment.Reference).IsEqualTo(new(null, ValidService));
			await Assert.That(shipment.Contact).IsEqualTo(new(ValidPhone, ValidEmail));
			await Assert.That(DateTimeOffset.UtcNow - shipment.RequestedAt < TimeSpan.FromSeconds(1)).IsTrue();
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenInvalid(Theory theory)
	{
		Assert.Throws<CustomValidationException<Shipment>>(() => CreateShipment(
				service: theory.Service,
				email: theory.Email,
				phone: theory.Phone,
				recipient: theory.Recipient,
				count: theory.Count,
				weight: theory.Weight,
				country: theory.Country,
				city: theory.City,
				street: theory.Street
			));
	}
}
