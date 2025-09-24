namespace CustomCADs.Delivery.Application.Contracts.Dtos;

public record ShipmentDto(
	string Id,
	string[] ParcelIds,
	decimal Price,
	DateOnly PickupDate,
	DateTime DeliveryDeadline
);
