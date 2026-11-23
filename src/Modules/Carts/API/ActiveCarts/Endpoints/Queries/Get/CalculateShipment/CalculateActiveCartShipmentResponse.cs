namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Queries.Get.CalculateShipment;

public record CalculateActiveCartShipmentResponse(
	string Service,
	double Total,
	string Currency,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
