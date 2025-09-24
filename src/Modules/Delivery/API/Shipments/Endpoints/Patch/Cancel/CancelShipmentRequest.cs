namespace CustomCADs.Delivery.API.Shipments.Endpoints.Patch.Cancel;

public record CancelShipmentRequest(
	Guid Id,
	string Comment
);
