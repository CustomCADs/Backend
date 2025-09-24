namespace CustomCADs.Delivery.API.Shipments.Dtos;

public record InfoResponse(
	int Count,
	double Weight,
	string Recipient
);
