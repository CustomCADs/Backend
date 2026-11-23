namespace CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentAddress(
	string Country = "",
	string Site = "",
	string AddressNote = "",
	double Longitude = 0,
	double Latitude = 0
);
