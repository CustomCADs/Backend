using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Behaviors.Activate;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	[Test]
	public void Activate_ShouldNotThrowException_WhenValidReferenceId()
	{
		CreateShipment().Activate(ValidReferenceId);
	}

	[Test]
	public async Task Activate_ShouldPopulateProperties()
	{
		var shipment = CreateShipment();

		shipment.Activate(ValidReferenceId);

		await Assert.That(shipment.Reference.Id).IsEqualTo(ValidReferenceId);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Activate_ShouldThrowException_WhenInvalidReferenceId(string referenceId)
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipment().Activate(referenceId)
		);
	}

	[Test]
	public void Activate_ShouldThrowException_WhenShipmentCancelled()
	{
		var shipment = CreateShipment().Cancel();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Activate(ValidReferenceId)
		);
	}

	[Test]
	public void Activate_ShouldThrowException_WhenShipmentActive()
	{
		var shipment = CreateShipment().Activate(ValidReferenceId);
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Activate(ValidReferenceId)
		);
	}

	[Test]
	public void Activate_ShouldThrowException_WhenShipmentDelivered()
	{
		var shipment = CreateShipment().Activate(ValidReferenceId).Deliver();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Activate(ValidReferenceId)
		);
	}
}
