namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.CalculateShipment;

public record CalculateCustomShipmentResponse(
	string Service,
	double Total,
	string Currency,
	DateOnly PickupDate,
	DateTimeOffset DeliveryDeadline
);
