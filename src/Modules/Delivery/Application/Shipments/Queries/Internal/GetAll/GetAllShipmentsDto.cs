using CustomCADs.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed record GetAllShipmentsDto(
	ShipmentId Id,
	Address Address,
	DateTimeOffset RequestedAt,
	string BuyerName
);
