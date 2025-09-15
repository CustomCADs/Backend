namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;

public sealed record GetShipmentTrackDto(
	string Message,
	string? Place
);
