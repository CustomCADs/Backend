namespace CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentContact(string? Phone, string? Email)
{
	internal ShipmentContact() : this(null, null) { }
}
