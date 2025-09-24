using CustomCADs.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Delivery.API.Shipments;

internal static class Mapper
{
	internal static InfoResponse ToResponse(this ShipmentInfo info)
		=> new(
			Count: info.Count,
			Weight: info.Weight,
			Recipient: info.Recipient
		);

	internal static AddressResponse ToResponse(this ShipmentAddress address)
		=> new(
			Country: address.Country,
			City: address.City,
			Street: address.Street
		);
}
