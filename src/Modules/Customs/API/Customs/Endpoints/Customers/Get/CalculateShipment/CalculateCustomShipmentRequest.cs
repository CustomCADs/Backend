namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.CalculateShipment;

public record CalculateCustomShipmentRequest(
	Guid Id,
	int Count,
	string Country,
	string City,
	string Street,
	Guid CustomizationId
);
