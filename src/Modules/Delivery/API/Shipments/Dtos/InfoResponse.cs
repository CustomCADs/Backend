namespace CustomCADs.Modules.Delivery.API.Shipments.Dtos;

public record InfoResponse(
	int Count,
	double Weight,
	string Recipient
);
