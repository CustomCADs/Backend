using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Delivery.Application.Shipments;

internal static class Mapper
{
	internal static GetAllShipmentsDto ToGetAllDto(this Shipment shipment)
		=> new(
			Id: shipment.Id,
			RequestedAt: shipment.RequestedAt,
			Status: shipment.Status,
			Info: shipment.Info,
			Address: shipment.Address
		);

	internal static CalculateShipmentDto ToDto(this CalculationDto calculation)
		=> new(
			Total: calculation.Price.Total,
			Currency: calculation.Price.Currency,
			PickupDate: calculation.PickupDate,
			DeliveryDeadline: calculation.DeliveryDeadline,
			Service: calculation.Service
		);
}
