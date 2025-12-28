using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed record GetAllShipmentsDto(
	ShipmentId Id,
	ShipmentInfo Info,
	ShipmentAddress Address,
	ShipmentStatus Status,
	DateTimeOffset RequestedAt
);
