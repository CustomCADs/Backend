namespace CustomCADs.Delivery.Endpoints.Shipments.Dtos;

public record InfoResponse(
	int Count,
	double Weight,
	string Recipient
);
