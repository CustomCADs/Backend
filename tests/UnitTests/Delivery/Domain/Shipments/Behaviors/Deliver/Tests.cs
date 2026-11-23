using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Behaviors.Deliver;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	[Test]
	public void Deliver_ShouldNotThrowException_WhenActive()
	{
		CreateShipment().Activate(ValidReferenceId).Deliver();
	}

	[Test]
	public void Deliver_ShouldThrowException_WhenShipmentAwaiting()
	{
		var shipment = CreateShipment();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Deliver()
		);
	}

	[Test]
	public void Deliver_ShouldThrowException_WhenShipmentCancelled()
	{
		var shipment = CreateShipment().Cancel();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Deliver()
		);
	}

	[Test]
	public void Deliver_ShouldThrowException_WhenShipmentDelivered()
	{
		var shipment = CreateShipment().Activate(ValidReferenceId).Deliver();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Deliver()
		);
	}
}
