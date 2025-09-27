namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Get.CalculateShipment;

public record CalculateActiveCartShipmentResponse(
	string Service,
	double Total,
	string Currency,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
