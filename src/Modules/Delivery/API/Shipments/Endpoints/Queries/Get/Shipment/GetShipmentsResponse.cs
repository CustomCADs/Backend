using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Shipment;

public record GetShipmentResponse(
	Guid Id,
	DateTimeOffset RequestedAt,
	ShipmentStatus Status,
	InfoResponse Info,
	double Fee
);
