namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetTracks;

public sealed record GetShipmentTracksQuery(
	ShipmentId Id
) : IQuery<Dictionary<DateTimeOffset, GetShipmentTracksDto>>;
