namespace CustomCADs.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentAddress(string Country, string City, string Street)
{
	internal ShipmentAddress() : this(string.Empty, string.Empty, string.Empty) { }
}
