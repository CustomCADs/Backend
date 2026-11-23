using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	private static readonly ShipmentDelivery delivery = new(DeliveryDestination.Office, null, 10);

	[Test]
	public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid()
	{
		Shipment.Create(
			buyerId: ValidBuyerId,
			delivery: delivery,
			service: ValidService,
			email: ValidEmail,
			phone: ValidPhone,
			recipient: ValidRecipient,
			count: MinValidCount,
			weight: MinValidWeight
		);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WhenShipmentIsValid()
	{
		Shipment shipment = Shipment.Create(
			buyerId: ValidBuyerId,
			delivery: delivery,
			service: ValidService,
			email: ValidEmail,
			phone: ValidPhone,
			recipient: ValidRecipient,
			count: MinValidCount,
			weight: MinValidWeight
		);

		using (Assert.Multiple())
		{
			await Assert.That(shipment.BuyerId).IsEqualTo(ValidBuyerId);
			await Assert.That(shipment.Delivery).IsEqualTo(delivery);
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
		Assert.Throws<CustomValidationException<Shipment>>(() => Shipment.Create(
				buyerId: ValidBuyerId,
				delivery: delivery,
				service: theory.Service,
				email: theory.Email,
				phone: theory.Phone,
				recipient: theory.Recipient,
				count: theory.Count,
				weight: theory.Weight
			));
	}
}
