using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Behaviors.Cancel;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	[Test]
	public void Cancel_ShouldNotThrowException_WhenAwaitingOrActive()
	{
		CreateShipment().Cancel();
		CreateShipment().Activate(ValidReferenceId).Cancel();
	}

	[Test]
	public void Cancel_ShouldThrowException_WhenShipmentCancelled()
	{
		var shipment = CreateShipment().Cancel();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Cancel()
		);
	}

	[Test]
	public void Cancel_ShouldThrowException_WhenShipmentDelivered()
	{
		var shipment = CreateShipment().Activate(ValidReferenceId).Deliver();
		Assert.Throws<CustomValidationException<Shipment>>(
			() => shipment.Cancel()
		);
	}
}
