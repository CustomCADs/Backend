using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.Application.Contracts.Dtos;

public record CalculateRequest(
	double TotalWeight,
	DeliveryDestination Destination,
	int? OfficeId,
	(string Country, string Site)? Address
);
