namespace CustomCADs.Delivery.Application.Contracts.Dtos;

public record CalculationDto(
	string Service,
	ShipmentPriceDto Price,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
