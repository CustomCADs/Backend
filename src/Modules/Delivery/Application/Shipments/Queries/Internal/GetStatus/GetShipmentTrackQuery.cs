namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;

public sealed record GetShipmentTrackQuery(
	ShipmentId Id
) : IQuery<Dictionary<DateTimeOffset, GetShipmentTrackDto>>;
