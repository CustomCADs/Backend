namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Queries.Get.CalculateShipment.ToOffice;

public record CalculateCustomShipmentToOfficeRequest(
	Guid Id,
	int Count,
	int OfficeId,
	Guid CustomizationId
);
