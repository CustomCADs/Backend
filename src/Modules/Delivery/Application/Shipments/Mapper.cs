using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Delivery.Application.Shipments;

internal static class Mapper
{
	extension(Shipment shipment)
	{
		internal GetAllShipmentsDto ToGetAllDto()
			=> new(
				Id: shipment.Id,
				RequestedAt: shipment.RequestedAt,
				Status: shipment.Status,
				Info: shipment.Info
			);
	}

	extension(AddressDto address)
	{
		internal ShipmentAddress ToAddress()
			=> new(
				Country: address.Country,
				Site: address.Site,
				AddressNote: address.AddressNote,
				Longitude: address.Coordinates.Longitude,
				Latitude: address.Coordinates.Latitude
			);
	}

	extension(CalculationDto calculation)
	{
		internal CalculateShipmentDto ToDto()
			=> new(
				Total: calculation.Price.Total,
				Currency: calculation.Price.Currency,
				PickupDate: calculation.PickupDate,
				DeliveryDeadline: calculation.DeliveryDeadline,
				Service: calculation.Service
			);
	}

}
