namespace CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentInfo(int Count, double Weight, string Recipient)
{
	internal ShipmentInfo() : this(0, 0, string.Empty) { }
}
