using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.API.Shipments.Endpoints.Get.Shipment;

public record GetShipmentsResponse(
	Guid Id,
	DateTimeOffset RequestedAt,
	ShipmentStatus Status,
	InfoResponse Info,
	AddressResponse Address
);
