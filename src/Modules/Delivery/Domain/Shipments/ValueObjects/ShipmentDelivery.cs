using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentDelivery(
	DeliveryDestination Destination = DeliveryDestination.Office,
	ShipmentAddress? Address = null,
	int? OfficeId = null
)
{
	private ShipmentDelivery() : this(DeliveryDestination.Office, null, null) { }
};
