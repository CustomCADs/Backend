using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetShipment;

public sealed record GetShipmentDto(
	ShipmentId Id,
	DateTimeOffset RequestedAt,
	ShipmentStatus Status,
	ShipmentInfo Info,
	double Fee
);

