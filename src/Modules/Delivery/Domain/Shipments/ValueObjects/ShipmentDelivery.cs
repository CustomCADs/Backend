using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentDelivery(
	DeliveryDestination Destination,
	ShipmentAddress? Address,
	int? OfficeId
)
{
	internal ShipmentDelivery() : this(DeliveryDestination.Office, null, null) { }
};
