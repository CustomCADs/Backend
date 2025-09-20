using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed record GetAllShipmentsDto(
	ShipmentId Id,
	ShipmentInfo Info,
	ShipmentAddress Address,
	ShipmentStatus Status,
	DateTimeOffset RequestedAt
);
