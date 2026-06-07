namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Mutations.Patch.Cancel;

public record CancelShipmentRequest(
	Guid Id,
	string Comment
);
