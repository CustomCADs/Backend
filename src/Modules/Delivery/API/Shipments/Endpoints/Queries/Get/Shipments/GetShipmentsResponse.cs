using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Shipments;

public record GetShipmentsResponse(
	Guid Id,
	DateTimeOffset RequestedAt,
	ShipmentStatus Status,
	InfoResponse Info
);
