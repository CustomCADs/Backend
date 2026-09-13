using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;
using SpeedyNET.Abstractions.UserConfigs.Pickup;
using SpeedyNET.Core.Enums;

namespace CustomCADs.Modules.Delivery.Infrastructure;

internal static class Mapper
{
	internal static DestinationType ToSpeedy(this DeliveryDestination destination)
		=> destination switch
		{
			DeliveryDestination.Address => DestinationType.ADDRESS,
			DeliveryDestination.Office => DestinationType.OFFICE,
			_ => DestinationType.APT,
		};

	internal static SpeedyShipmentAddress ToSpeedy(this ShipmentAddress address)
		=> new(
			Country: address.Country,
			Site: address.Site,
			AddressNote: address.AddressNote,
			Coordinates: (
				Longitude: address.Longitude,
				Latitude: address.Latitude
			)
		);

}
