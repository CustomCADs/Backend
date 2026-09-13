namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Queries.Get.CalculateShipment.ToAddress;

public record CalculateCustomShipmentToAddressRequest(
	Guid Id,
	int Count,
	string Country,
	string Site,
	Guid CustomizationId
);
