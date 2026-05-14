using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Shipment;

public record GetShipmentsResponse(
	Guid Id,
	DateTimeOffset RequestedAt,
	ShipmentStatus Status,
	InfoResponse Info,
	AddressResponse Address
);
