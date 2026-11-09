namespace CustomCADs.Shared.Application.Dtos.Delivery;

public record CalculateShipmentDto(
	double Total,
	double Amount,
	double Vat,
	string Currency,
	string Service,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
