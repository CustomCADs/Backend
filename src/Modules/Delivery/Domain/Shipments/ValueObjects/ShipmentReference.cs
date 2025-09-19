namespace CustomCADs.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentReference(string? Id, string Service)
{
	internal ShipmentReference() : this(null, string.Empty) { }
};
